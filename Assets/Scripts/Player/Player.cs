using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region componenets

    private PlayerInputHandler _inputHandler;
    private Rigidbody _rb;
    private Camera _camera;
    private Collider _collider;
    private CustomGravityObject _customGravityObject;
    private Transform _weaponHolder;

    [SerializeField] private PlayerData _playerData;

    private PlayerMovement _movement;
    private PlayerCombat _combat;
    public PlayerHealth _health;

    #endregion

    [SerializeField] private bool _isCurrentPlayer;


    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _customGravityObject = GetComponent<CustomGravityObject>();
        _camera = Camera.main;
        _weaponHolder = transform.Find("WeaponHolder");

        _movement = new PlayerMovement(_inputHandler, _rb, _camera, _collider, _customGravityObject, _playerData);
        _combat = new PlayerCombat(_inputHandler, this);
        _health = new PlayerHealth(this, _playerData._hp);
    }

    private void OnEnable() => PlayerManager._activePlayers.Add(this);
    private void OnDisable() => PlayerManager._activePlayers.Remove(this);

    private void Start()
    {
        AddWeapon(_playerData._startWeapon);
        _combat.InitWeaponInventory();
        
    }

    private void Update()
    {
        if (_isCurrentPlayer)
        {
            _movement.Updates();
            _combat.Updates();
        }
    }

    private void FixedUpdate()
    {
        if (_isCurrentPlayer)
        {
            _movement.PhysicsUpdates();
        }
    }

    public void KnockBack(Vector3 dir, float force)
    {
        _rb.AddForce(dir * force, ForceMode.Impulse);
    }

    public void AddWeapon(GameObject weapon)
    {
        var newWeapon = Instantiate(weapon, _weaponHolder);
        _combat.AddWeaponToInventory(newWeapon);
    }

    public void Die()
    {
        //Do death things...

        Destroy(gameObject);
        PlayerManager.GetInstance()._currentPlayerCount--;
    }

    public void SetCurrentPlayer(bool b)
    {
        _isCurrentPlayer = b;
    }
}
