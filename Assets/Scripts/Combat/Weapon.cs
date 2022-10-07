using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Mesh _bulletMesh;
    [SerializeField] private Material _bulletMaterial;
    [SerializeField] private GameObject _hitParticles;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Vector3 _aimOffset = new Vector3(0f, 0.4f, 0f);
    [Space]
    [SerializeField] private float _damage;
    [Space]
    [SerializeField] private bool _explosive;
    [SerializeField] private float _explosionRange;
    [Space]
    [SerializeField] private float _knockBack;
    [Space]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletGravityScale;
    [Space]
    [SerializeField] private bool _multipleBullets;
    [SerializeField] private float _timeBetweenBullets;
    [SerializeField] private int _bulletCount;

    private Camera _cam;
    private Vector3 middlePoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private LineRenderer _aimLine;

  

    private void Awake()
    {
        _cam = Camera.main;
        _aimLine = GetComponent<LineRenderer>();
        _aimLine.useWorldSpace = false;
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    #region Shoot
    public void Shoot()
    {
        SpawnBullet();
        if (_multipleBullets)
        {
            StartCoroutine(MultipleBullets());
        }
    }

    private void SpawnBullet()
    {
        var bulletObject = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.Init(_bulletSpeed, transform.forward, _damage, _bulletMesh, _bulletMaterial,
            _hitParticles, _explosive, _explosionRange, _knockBack, _bulletGravityScale);
    }

    IEnumerator MultipleBullets()
    {
        for (int i = 0; i < _bulletCount - 1; i++)
        {
            SpawnBullet();
            yield return new WaitForSeconds(_timeBetweenBullets);
        }
    }

    #endregion

    #region Aim
    public void Aim()
    {
        RaycastHit hit;
        var ray = _cam.ScreenPointToRay(middlePoint);
        Vector3 aimTarget;

        if (Physics.Raycast(ray, out hit))
        {
            aimTarget = hit.point;
            transform.rotation = Quaternion.LookRotation(aimTarget + _aimOffset);
        }
    }
    public void ResetAim()
    {
        transform.rotation = Quaternion.identity;
    }

    #endregion

}
