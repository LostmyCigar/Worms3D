using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Weapon
{
    [SerializeField] private GameObject _bazookaBullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletGravityScale;

    public override void Shoot()
    {
        var bulletObject = Object.Instantiate(_bazookaBullet, _shootPoint.position, Quaternion.identity);
        var bullet = bulletObject.GetComponent<BazookaBullet>();
        bulletObject.GetComponent<CustomGravityObject>().gravityScale = _bulletGravityScale;
        bullet.Init(_bulletSpeed, transform.forward);
    }
}
