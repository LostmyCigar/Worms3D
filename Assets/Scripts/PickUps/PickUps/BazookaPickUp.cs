using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaPickUp : PickUp
{
    [SerializeField] private GameObject _bazookaPrefab;
    protected override void CollidedWithPlayer(Player player)
    {
        player._combat.AddWeaponToInventory(_bazookaPrefab);
        base.CollidedWithPlayer(player);
    }
}
