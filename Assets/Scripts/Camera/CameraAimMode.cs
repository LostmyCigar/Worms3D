using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAimMode : CameraMode
{

    private float _lookAngle;
    private float _pivotAngle;
    private Vector3 rotation = Vector3.zero;
    public CameraAimMode(CameraHandler handler, CameraInputHandler inputHandler, PlayerData playerData, Transform baseTransform, Transform positionTransform) : base(handler, inputHandler, playerData, baseTransform, positionTransform)
    {
    }

    public override void Update()
    {
        RotateCamera();
        base.Update();
    }
    protected override void ChangeStateCheck()
    {
        if (!_inputHandler._cameraControlInput)
        {
            _handler.SetNewCameraState(_handler._normalMode);
        }
    }

    private void RotateCamera()
    {
        var mouseX = _inputHandler._mouseDelta.x;
        var mouseY = _inputHandler._mouseDelta.y;

        _lookAngle += (mouseX * _playerData._cameraAimLookSpeed) * Time.deltaTime;
        _pivotAngle += (mouseY * _playerData._cameraAimLookSpeed) * Time.deltaTime;
        _pivotAngle = Mathf.Clamp(_pivotAngle, _playerData._cameraAimPivotMin, _playerData._cameraAimPivotMax);

        rotation.y = -_lookAngle;
        rotation.x = _pivotAngle;
        Quaternion rotationQuaternion = Quaternion.Euler(rotation);
        _positionTransform.rotation = rotationQuaternion; //Im about 80% sure that there is something i can use to avoid code repetition between camera modes
                                                          //since the rotation function is very similar, but i cant remeber right now
    }
}
