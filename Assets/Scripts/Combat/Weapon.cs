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
    private LineRenderer _aimLine;
    [SerializeField] private LayerMask _groundLayer;
    private RaycastHit hit;



    private void Awake()
    {
        _cam = Camera.main;
        _aimLine = GetComponent<LineRenderer>();
        _aimLine.useWorldSpace = true;
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
        yield return new WaitForSeconds(_timeBetweenBullets);
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

        Vector2 centerPoint = new Vector3(Screen.width / 2, Screen.height / 2, -1);
        Ray ray = _cam.ScreenPointToRay(centerPoint);


        if (Physics.Raycast(ray, out hit, 1024f, _groundLayer))
        {
            transform.LookAt(hit.point);
        }// else transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, 0.3f);




    }

    #endregion
}
