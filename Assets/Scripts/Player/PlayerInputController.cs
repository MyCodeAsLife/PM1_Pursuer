using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController
{
    private PlayerInputActions _inputActions;
    private List<Action<InputAction.CallbackContext>> _subscribeFunctions = new();

    public PlayerInputController()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
    }

    public void SubscribeOnMoveStart(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.started += func;
        }
    }

    public void SubscribeOnMovePerformed(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.performed += func;
        }
    }

    public void SubscribeOnMoveCanceled(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.canceled += func;
        }
    }

    public void UnSubscribeOnMoveStart(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.started -= func;
        }
    }

    public void UnSubscribeOnMovePerformed(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func))
        {
            _subscribeFunctions.Remove(func);
            _inputActions.BaseControl.Move.performed -= func;
        }
    }

    public void UnSubscribeOnMoveCanceled(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.canceled -= func;
        }
    }

    public Vector2 GetMovementVector() => _inputActions.BaseControl.Move.ReadValue<Vector2>();
}
