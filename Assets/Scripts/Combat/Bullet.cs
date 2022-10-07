using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    private GameObject _hitParticles;

    private float _damage;
    private bool _explosive;
    private float _explosionRange;
    private float _knockBack;
    private float _bulletGravityScale;
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    public void Init(float speed, Vector3 dir, float damage, Mesh mesh, Material material,
        GameObject hitParticles, bool shouldExplode, float explosionRange,
        float knockBack, float gravityScale) 
    {
        _damage = damage;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = material;
        GetComponent<CustomGravityObject>().gravityScale = gravityScale;
        _explosive = shouldExplode;
        _explosionRange = explosionRange;
        _knockBack = knockBack;
        _hitParticles = hitParticles;

        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            var player = other.GetComponent<Player>();
            OnCollisionPlayer(player);
        } else if (other.CompareTag("Ground"))
        {
            OnCollisionWall();
        }
    }

    private void OnCollisionPlayer(Player player)
    {
        player._health.TakeDamage(_damage);

        var dir = (transform.position - player.transform.position).normalized;
        player._movement.KnockBack(_knockBack, dir);

        Instantiate(_hitParticles, transform.position, Quaternion.identity);

        if (_explosive)
        {
            Explode();
        }
    }

    private void OnCollisionWall()
    {
        if (_explosive)
        {
            Explode();
        }
        Destroy(gameObject);
    }

    private void Explode() 
    { 

    }
}
