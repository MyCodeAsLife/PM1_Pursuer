using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController
{
    private PlayerInputActions _inputActions;
    private Vector2 _lookDirection;
    private Vector2 _moveDirection;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private float _moveSpeed;
    private Transform transform;

    public Action<Vector2> MovePerformed;
    public Action<Vector2> LookPerformed;

    public PlayerInputController()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();                         // Или Активировать/дезактивировать из-вне??

        _inputActions.BaseControl.Move.performed += OnMove;
        _inputActions.BaseControl.Look.performed += OnLook;
    }

    ~PlayerInputController()
    {
        _inputActions.BaseControl.Move.performed -= OnMove;
        _inputActions.BaseControl.Look.performed -= OnLook;

        _inputActions.Disable();
    }

    //private void OnEnable()
    //{
    //    _inputActions = new PlayerInputActions();
    //    _inputActions.Enable();

    //    _inputActions.BaseControl.Move.performed += OnMove;
    //    _inputActions.BaseControl.Look.performed += OnLook;
    //}

    //private void OnDisable()
    //{
    //    _inputActions.BaseControl.Move.performed -= OnMove;
    //    _inputActions.BaseControl.Look.performed -= OnLook;

    //    _inputActions.Disable();
    //}

    //private void Update()
    //{
    //    _lookDirection = _inputActions.BaseControl.Look.ReadValue<Vector2>();
    //    _moveDirection = _inputActions.BaseControl.Move.ReadValue<Vector2>();

    //    Look();
    //    Move();
    //}

    void Look()
    {
        float scaleLookSpeed = _lookSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(-_lookDirection.y, _lookDirection.x, 0f) * scaleLookSpeed;

        transform.Rotate(offset * scaleLookSpeed);
    }

    void Move()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * scaledMoveSpeed;

        transform.Translate(offset * scaledMoveSpeed);
    }

    void OnLook(InputAction.CallbackContext context)
    {
        _lookDirection = context.action.ReadValue<Vector2>();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.action.ReadValue<Vector2>();
    }
}
