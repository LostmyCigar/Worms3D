using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Bullet
{
    public float _damage;
    protected override void OnCollisionPlayer(Collider playerCollider)
    {
        base.OnCollisionWall();

        var player = playerCollider.GetComponent<Player>();
        player._health.TakeDamage(_damage);
    }

    protected override void OnCollisionWall()
    {
        base.OnCollisionWall();
        Destroy(gameObject);
    }
}
