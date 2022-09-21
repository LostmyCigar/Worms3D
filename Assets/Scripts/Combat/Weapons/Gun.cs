using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject _gunBullet;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletGravityScale;
    [SerializeField] private float _timeBetweenBullets;
    [SerializeField] private int _bulletCount;
    public override void Shoot()
    {
        StartCoroutine(MultipleBullets());
    }

    IEnumerator MultipleBullets()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            SpawnBullet();
            yield return new WaitForSeconds(_timeBetweenBullets);
        }
    }

    private void SpawnBullet()
    {
        var bulletObject = Object.Instantiate(_gunBullet, _shootPoint.position, Quaternion.identity);
        var bullet = bulletObject.GetComponent<GunBullet>();
        bulletObject.GetComponent<CustomGravityObject>().gravityScale = _bulletGravityScale;
        bullet.Init(_bulletSpeed, transform.forward);
        bullet._damage = _bulletDamage;
    }
}
