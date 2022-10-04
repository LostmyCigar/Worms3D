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
    private Collider _collider;
    private Camera _camera;
    private CustomGravityObject _customGravityObject;
    private Transform _weaponHolder;

    [SerializeField] private PlayerData _playerData;

    public PlayerMovement _movement;
    public PlayerCombat _combat;
    public PlayerHealth _health;

    #endregion

    [SerializeField] private bool _isCurrentPlayer;


    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _collider = GetComponent<Collider>();
        _customGravityObject = GetComponent<CustomGravityObject>();
        _weaponHolder = transform.Find("WeaponHolder");

        _movement = new PlayerMovement(_inputHandler, _rb, _camera, _collider, _customGravityObject, _playerData);
        _combat = new PlayerCombat(_inputHandler, this);
        _health = new PlayerHealth(this, _playerData._hp);
    }

    private void OnEnable() => PlayerManager._activePlayers.Add(this);
    private void OnDisable() => PlayerManager._activePlayers.Remove(this);

    private void Start()
    {
        _combat.AddWeaponToInventory(_playerData._startWeapon);
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


    public GameObject CreateWeapon(GameObject weapon)
    {
        return Instantiate(weapon, _weaponHolder);
    }

    public void StartPlayerTurn()
    {
        _weaponHolder.gameObject.SetActive(true);
        _isCurrentPlayer = true;
    }

    public void EndPlayerTurn()
    {
        _isCurrentPlayer = false;
        _weaponHolder.gameObject.SetActive(false);
    }

    public void Die()
    {
        //Do death things...
        if (_isCurrentPlayer)
        {
            PlayerManager.GetInstance().EndTurn();
        }
        PlayerManager.GetInstance()._currentPlayerCount--;


        Destroy(gameObject);
    }

}
