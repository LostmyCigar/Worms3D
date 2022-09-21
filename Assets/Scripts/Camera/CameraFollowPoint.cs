using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour
{
    private Transform _currentPlayerTransform;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _smoothTime = 0.3f;

    void Start()
    {
        _currentPlayerTransform = PlayerManager._currentPlayer.transform;
        PlayerManager.GetInstance().OnNewTurn += GetNewCurrentPlayer;
    }


    private void LateUpdate()
    {
        if (_currentPlayerTransform != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _currentPlayerTransform.position, ref _velocity, _smoothTime);
        }
    }

    private void GetNewCurrentPlayer()
    {
        _currentPlayerTransform = PlayerManager._currentPlayer.transform;
    }
}
