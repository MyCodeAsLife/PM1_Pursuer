using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover
{
    private CharacterController _controller;
    private Transform _transform;
    private Vector2 _moveDirection;
    private float _rotateSpeed;
    private float _moveSpeed;

    public event Action MoveStarted;
    public event Action MoveCanceled;
    private event Action Moved;

    public PlayerMover(CharacterController controller, Transform transform)
    {
        _controller = controller;
        _transform = transform;
        _rotateSpeed = 0.05f;
        _moveSpeed = 5f;

        PlayerInputController.SubscribeOnMoveStart(OnMoveStart);
        PlayerInputController.SubscribeOnMovePerformed(OnMove);
        PlayerInputController.SubscribeOnMoveCanceled(OnMoveCanceled);
    }

    ~PlayerMover()
    {
        PlayerInputController.UnSubscribeOnMoveStart(OnMoveStart);
        PlayerInputController.UnSubscribeOnMovePerformed(OnMove);
        PlayerInputController.UnSubscribeOnMoveCanceled(OnMoveCanceled);
    }

    public void Tick()
    {
        Moved?.Invoke();
    }

    private void Move()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        _controller.Move(_transform.forward * scaledMoveSpeed);
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(-_moveDirection.x, 0f, -_moveDirection.y);
        Quaternion rotation = Quaternion.LookRotation(direction * Time.deltaTime);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _rotateSpeed);
    }

    private void OnMoveStart(InputAction.CallbackContext context)
    {
        MoveStarted?.Invoke();
        Moved += Move;
        Moved += Rotate;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.action.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveCanceled?.Invoke();
        Moved -= Move;
        Moved += Rotate;
    }
}
