using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayerInputController
{
    private static PlayerInputActions _inputActions;
    private static List<Action<InputAction.CallbackContext>> _subscribeFunctions = new();

    static PlayerInputController()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
    }

    public static void SubscribeOnMoveStart(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.started += func;
        }
    }

    public static void SubscribeOnMovePerformed(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.performed += func;
        }
    }

    public static void SubscribeOnMoveCanceled(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.canceled += func;
        }
    }

    public static void UnSubscribeOnMoveStart(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.started -= func;
        }
    }

    public static void UnSubscribeOnMovePerformed(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func))
        {
            _subscribeFunctions.Remove(func);
            _inputActions.BaseControl.Move.performed -= func;
        }
    }

    public static void UnSubscribeOnMoveCanceled(Action<InputAction.CallbackContext> func)
    {
        if (_subscribeFunctions.Contains(func) == false)
        {
            _subscribeFunctions.Add(func);
            _inputActions.BaseControl.Move.canceled -= func;
        }
    }

    public static Vector2 GetMovementVector() => _inputActions.BaseControl.Move.ReadValue<Vector2>();
}
