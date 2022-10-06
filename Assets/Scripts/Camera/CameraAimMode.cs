using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAimMode : CameraMode
{

    private float _lookAngle;
    private float _pivotAngle;
    private Vector3 rotation = Vector3.zero;
    public CameraAimMode(CameraHandler handler, CameraInputHandler inputHandler, PlayerData playerData, Camera camera, Transform positionTransform) : base(handler, inputHandler, playerData, camera, positionTransform)
    {
    }

    public override void Enter()
    {
        _camera.fieldOfView = _playerData._aimFOV;
        _startRotation = _positionTransform.rotation;
    }

    public override void Exit()
    {
        _lookAngle = _startRotation.eulerAngles.y;
        _pivotAngle = _startRotation.eulerAngles.x;
        _positionTransform.rotation = _startRotation;
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
        _lookAngle = Mathf.Clamp(_lookAngle, _playerData._cameraAimLookMin, _playerData._cameraAimLookMax);


        _pivotAngle += (mouseY * _playerData._cameraAimLookSpeed) * Time.deltaTime;
        _pivotAngle = Mathf.Clamp(_pivotAngle, _playerData._cameraAimPivotMin, _playerData._cameraAimPivotMax);

        rotation.y = _lookAngle;
        rotation.x = -_pivotAngle;
        Quaternion rotationQuaternion = Quaternion.Euler(rotation);
        _positionTransform.rotation = rotationQuaternion; //Im about 80% sure that there is something i can use to avoid code repetition between camera modes
                                                          //since the rotation function is very similar, but i cant remeber right now
    }

}
