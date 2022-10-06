using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNormalMode : CameraMode
{
    private float _lookAngle;
    private float _pivotAngle;
    private Vector3 rotation = Vector3.zero;

    public CameraNormalMode(CameraHandler handler, CameraInputHandler inputHandler, PlayerData playerData, Transform baseTransform, Transform positionTransform) : base(handler, inputHandler, playerData, baseTransform, positionTransform)
    {
    }

    public override void Enter()
    {
        
    }
    public override void Update()
    {
        RotateCamera();
        base.Update();
    }


    private void RotateCamera()
    {
        var mouseX = _inputHandler._mouseDelta.x;
        var mouseY = _inputHandler._mouseDelta.y;

        _lookAngle += (mouseX * _playerData._cameraLookSpeed) * Time.deltaTime;
        _pivotAngle += (mouseY * _playerData._cameraLookSpeed) * Time.deltaTime;
        _pivotAngle = Mathf.Clamp(_pivotAngle, _playerData._cameraPivotMin, _playerData._cameraPivotMax);

        rotation.y = -_lookAngle;
        rotation.x = _pivotAngle;
        Quaternion rotationQuaternion = Quaternion.Euler(rotation);
        _baseTransform.rotation = rotationQuaternion;
    }

    protected override void ChangeStateCheck()
    {
        if (_inputHandler._cameraControlInput)
        {
            _handler.SetNewCameraState(_handler._aimMode);
        }
    }
}
