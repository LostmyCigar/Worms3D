using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{

    [Header("Turns")]
    public float _turnTime;

    [Header("Movement")]
    [Range(5f, 30f)]
    public float _moveSpeed;
    [Range(1f, 20f)]
    public float _rotationSpeed;

    [Header("Jumping")]
    public LayerMask _ground;
    [Tooltip("Jumpforce. This makes the jump faster and higher, combine with Normal Gravity Scale to adjust both height and speed")]
    public float _jumpForce;
    [Tooltip("Gravity when falling. This makes the jump less floaty")]
    public float _fallingGravityScale;
    public float _normalGravityScale;
    public float _startFallingVelocity;

    [Header("Combat")]
    public float _hp;
    public GameObject _startWeapon;

    [Header("Normal Camera")]
    [Range(0.03f, 0.2f)] public float _cameraFollowSpeed;

    [Range(0.1f, 5f)] public float _cameraLookSpeed;
    [Range(0.07f, 5f)] public float _cameraPivotSpeed;

    [Range(-80, 0)] public float _cameraPivotMin;
    [Range(0, 80)] public float _cameraPivotMax;

    [Header("Aiming Camera")]
    [Range(0.1f, 5f)] public float _cameraAimLookSpeed;
    [Range(0.07f, 5f)] public float _cameraAimPivotSpeed;

    [Range(-120, -50)] public float _cameraAimPivotMin;
    [Range(50, 120)] public float _cameraAimPivotMax;

}
