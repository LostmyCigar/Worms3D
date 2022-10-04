using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private float _smoothTime = 0.3f;

    private Transform _target;
    private bool _acceptInput;
    
    private Vector3 _velocity = Vector3.zero;


    void Start()
    {
        _target = PlayerManager._currentPlayer.transform;

        PlayerManager.GetInstance().OnStartTurn += SetNewCameraTarget;
        PickUpManager.GetInstance().OnPickUpSpawn += SetNewCameraTarget;
    }


    private void LateUpdate()
    {
        if (_target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _velocity, _smoothTime);
        }
    }
    private void SetNewCameraTarget(Transform target)
    {
        _target = target;
    }
}
