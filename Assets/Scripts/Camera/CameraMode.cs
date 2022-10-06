using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class CameraMode
{
    protected CameraInputHandler _inputHandler;
    protected CameraHandler _handler;
    protected PlayerData _playerData;
    protected Transform _baseTransform;
    public Transform _positionTransform;

    public CameraMode(CameraHandler handler, CameraInputHandler inputHandler, PlayerData playerData, Transform baseTransform, Transform positionTransform)
    {
        _handler = handler;
        _inputHandler = inputHandler;
        _playerData = playerData;
        _baseTransform = baseTransform;
        _positionTransform = positionTransform;
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
