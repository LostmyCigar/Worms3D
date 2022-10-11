using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUp : PickUp
{
    [SerializeField] private float _healValue;
    protected override void CollidedWithPlayer(Player player)
    {
        player._health.Health += _healValue;

        base.CollidedWithPlayer(player);
    }
}
