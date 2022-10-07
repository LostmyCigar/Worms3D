using UnityEngine;

public abstract class CameraMode
{
    protected CameraInputHandler _inputHandler;
    protected CameraHandler _handler;
    protected PlayerData _playerData;
    protected Transform _cameraTransform;
    protected Camera _camera;
    protected Transform _baseTransform;
    public Transform _positionTransform;

    protected static float _lookAngle;
    protected static float _pivotAngle;
    protected static Vector3 rotation = Vector3.zero;
    //  protected Quaternion _startRotation;

    public CameraMode(CameraHandler handler, CameraInputHandler inputHandler, PlayerData playerData, Camera camera, Transform baseTransform, Transform positionTransform)
    {
        _handler = handler;
        _inputHandler = inputHandler;
        _playerData = playerData;
        _baseTransform = baseTransform;
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
