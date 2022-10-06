using UnityEngine;

public abstract class CameraMode
{
    protected CameraInputHandler _inputHandler;
    protected CameraHandler _handler;
    protected PlayerData _playerData;
    protected Transform _cameraTransform;
    protected Camera _camera;
    public Transform _positionTransform;

    protected Quaternion _startRotation;

    public CameraMode(CameraHandler handler, CameraInputHandler inputHandler, PlayerData playerData, Camera camera, Transform positionTransform)
    {
        _handler = handler;
        _inputHandler = inputHandler;
        _playerData = playerData;
        _positionTransform = positionTransform;
        _camera = camera;
    }
    

    public virtual void Enter()
    {
    }
    public virtual void Update()
    {
        ChangeStateCheck();
    }

    public virtual void Exit()
    {
    }

    protected virtual void ChangeStateCheck()
    {

    }
}
