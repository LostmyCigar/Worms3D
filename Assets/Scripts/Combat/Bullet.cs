using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    protected Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 3);
    }
    public virtual void Init(float speed, Vector3 dir) 
    {
        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    protected void OnTriggerEnter(Collider other)
    {

        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            OnCollisionPlayer(other);
        } else if (other.CompareTag("Ground"))
        {
            OnCollisionWall();
        }
    }

    protected virtual void OnCollisionPlayer(Collider player)
    {
        Debug.Log("Hit Player!");
    }

    protected virtual void OnCollisionWall()
    {

    }
}
