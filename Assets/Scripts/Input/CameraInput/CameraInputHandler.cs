using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{

    public Vector2 _mouseDelta;
    public bool _cameraControlInput;

    public void CameraControlInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _cameraControlInput = true;
        }
        else if (context.canceled)
        {
            _cameraControlInput = false;
        }
    }

    public void MouseDeltaInput(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }
}
