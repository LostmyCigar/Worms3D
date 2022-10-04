using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector3 _moveDir;
    public float _mouseX;
    public float _mouseY;
    public bool _leftClick;
    public bool _jumpInput;
    public bool _weaponSwap;

    private void MoveInput(InputAction.CallbackContext context)
    {
        _moveDir.x = context.ReadValue<Vector2>().x;
        _moveDir.z = context.ReadValue<Vector2>().y;
        _moveDir.Normalize();
    }
    private void JumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jumpInput = true;
        } else if (context.canceled)
        {
            _jumpInput = false;
        }
    }
    private void LeftClickInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _leftClick = true;
        }
        else if (context.canceled)
        {
            _leftClick = false;
        }
    }

    private void WeaponSwapInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _weaponSwap = true;
        }
        else if (context.canceled)
        {
            _weaponSwap = false;
        }
    }


    public void UseJumpInput() => _jumpInput = false;
    public void UseLeftClickInput() => _leftClick = false;
    public void UseWeaponSwapInput() => _weaponSwap = false;
}
