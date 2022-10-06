using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private static CameraHandler _instance;

    [SerializeField] private PlayerData _playerData;
    private CameraInputHandler _inputHandler;
    private CameraMode _state;

    public CameraNormalMode _normalMode;
    [SerializeField] private Transform _normalPoint;

    public CameraAimMode _aimMode;
    [SerializeField] private Transform _aimPoint;

    private Transform _cameraPositionTarget;
    [SerializeField] private Transform _cameraTransform;

    private Transform _target;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _camVelocity = Vector3.zero;




    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);


        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _inputHandler = GetComponent<CameraInputHandler>();
        if (_aimPoint == null) Debug.LogError("AimPoint is null");

        _normalMode = new CameraNormalMode(this, _inputHandler, _playerData, transform, _normalPoint);
        _aimMode = new CameraAimMode(this, _inputHandler, _playerData, transform, _aimPoint);

        _state = _normalMode;
        _cameraPositionTarget = _state._positionTransform;
    }


    void Start()
    {
        _target = PlayerManager._currentPlayer.transform;

        PlayerManager.GetInstance().OnStartTurn += SetNewCameraTarget;
        PickUpManager.GetInstance().OnPickUpSpawn += SetNewCameraTarget;
    }


    private void LateUpdate()
    {
        FollowTarget();
        CameraPosition();
        _state.Update();
    }

    private void FollowTarget()
    {
        if (_target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _velocity, _playerData._cameraFollowSpeed);
        }
    }
    private void CameraPosition()
    {
        if (_cameraPositionTarget != null)
        {
            _cameraTransform.position = Vector3.SmoothDamp(_cameraTransform.position, _cameraPositionTarget.position, ref _camVelocity, _playerData._cameraFollowSpeed);
        }
    }
    public void SetNewCameraState(CameraMode newState)
    {
        _state.Exit();
        _state = newState;
        _state.Enter();
        _cameraPositionTarget = _state._positionTransform;
        _cameraTransform.parent = _cameraPositionTarget;
    }
    private void SetNewCameraTarget(Transform target)
    {
        _target = target;
    }


}
