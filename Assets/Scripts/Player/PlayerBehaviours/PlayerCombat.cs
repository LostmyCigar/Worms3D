using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat
{
    private PlayerInputHandler _inputHandler;
    private Player _player;
    private List<Weapon> _weaponInventory = new List<Weapon>();
    private Weapon _activeWeapon;
    private int _activeWeaponIndex;


    public PlayerCombat(PlayerInputHandler inputHandler, Player player)
    {
        _inputHandler = inputHandler;
        _activeWeaponIndex = 0;
        _player = player;

    }

    public void Updates()
    {
        HandleAim();
        HandleShoot();
        SwitchActiveWeapon();
    }
    
    public void InitWeaponInventory()
    {
        _activeWeapon = _weaponInventory[_activeWeaponIndex];
    }

    public void AddWeaponToInventory(GameObject weapon)
    {
        var newWeapon = _player.CreateWeapon(weapon);
        var weaponScript = newWeapon.GetComponent<Weapon>();
        if (_weaponInventory.Count >= 1)
        {
            weaponScript.Disable();
        }
        _weaponInventory.Add(weaponScript);
    }

    private void SwitchActiveWeapon()
    {
        if (_inputHandler._weaponSwap)
        {
            _activeWeapon.Disable();
            _activeWeaponIndex++;
            _activeWeaponIndex %= _weaponInventory.Count;
            _activeWeapon = _weaponInventory[_activeWeaponIndex];
            _activeWeapon.Enable();
            _inputHandler.UseWeaponSwapInput();
        }
    }

    private void HandleAim()
    {
        if (_inputHandler._rightClick)
        {
            _activeWeapon.Aim();
            Debug.Log("Aiming");
        }
    }

    private void HandleShoot()
    {
        if (_inputHandler._leftClick)
        {
            _activeWeapon.Shoot();
          //  _player.KnockBack()
            _inputHandler.UseLeftClickInput();
            PlayerManager.GetInstance().EndTurn();
        }
    }


}
