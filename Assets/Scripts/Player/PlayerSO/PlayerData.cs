using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{



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
}
