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
    }

    public void Tick()
    {
        Moved?.Invoke();
        Falling();
    }

    public void ColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody != null)
            hit.rigidbody.velocity = _controller.velocity;
    }

    public void OnMoveStart(InputAction.CallbackContext context)
    {
        MoveStarted?.Invoke();
        Moved += Move;
        Moved += Rotate;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.action.ReadValue<Vector2>();
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveCanceled?.Invoke();
        Moved -= Move;
        Moved += Rotate;
    }

    private void Move()
    {
        Vector3 playerSpeed = new Vector3(-_moveDirection.x, 0f, -_moveDirection.y);
        playerSpeed *= Time.deltaTime * _moveSpeed;

        if (_controller.isGrounded)
            _controller.Move(playerSpeed);
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(-_moveDirection.x, 0f, -_moveDirection.y);
        Quaternion rotation = Quaternion.LookRotation(direction * Time.deltaTime);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _rotateSpeed);
    }

    private void Falling()
    {
        _controller.Move(Physics.gravity * Time.deltaTime);
    }
}