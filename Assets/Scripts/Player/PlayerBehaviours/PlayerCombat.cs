using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        HandleShoot();
        SwitchActiveWeapon();
    }
    
    public void InitWeaponInventory()
    {
        _activeWeapon = _weaponInventory[_activeWeaponIndex];
    }

    public void AddWeaponToInventory(GameObject weapon)
    {
        var newWeapon = weapon.GetComponent<Weapon>();
        if (_weaponInventory.Count >= 1)
        {
            newWeapon.Disable();
        }
        _weaponInventory.Add(newWeapon);
    }

    private void SwitchActiveWeapon()
    {
        if (_inputHandler._rightClick)
        {
            _activeWeapon.Disable();
            _activeWeaponIndex++;
            _activeWeaponIndex %= _weaponInventory.Count;
            _activeWeapon = _weaponInventory[_activeWeaponIndex];
            _activeWeapon.Enable();
            _inputHandler.UseRightClickInput();
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
