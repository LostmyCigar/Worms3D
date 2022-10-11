using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(CustomGravityObject))]
[RequireComponent(typeof(MeshRenderer))]
public class PickUp : MonoBehaviour
{
    protected CustomGravityObject _gravity;
    protected Collider _collider;
    protected Rigidbody _rb;

    protected bool _rotate;

    protected bool _isFalling;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _gravity = GetComponent<CustomGravityObject>();
        _rb = GetComponent<Rigidbody>();
        _collider.isTrigger = true;
    }

    private void Start()
    {
        StartCoroutine(FallAcceleration());
    }

    private void FixedUpdate()
    {
        if (_rotate)
        {
            transform.Rotate(new Vector3(0, 0.5f, 0), Space.World);
        }
    }

    IEnumerator FallAcceleration()
    {
        while (_isFalling)
        {
            Debug.Log("Falling Coroutine called");
            _gravity.gravityScale += 0.2f;
            yield return new WaitForFixedUpdate();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            CollidedWithPlayer(player);
        } else if (other.CompareTag("Ground"))
        {
            _gravity.gravityScale = 0;
            _isFalling = false;
            _rb.velocity = Vector3.zero;
            _rotate = true;
        }
    }

   /* private void OnTriggerStay(Collider other)
    {
        var emptyPos = other.ClosestPointOnBounds(transform.position);
        var offset = (transform.position - emptyPos).normalized * 1.5f;

        transform.position = emptyPos + offset;
    } */
    protected virtual void CollidedWithPlayer(Player player)
    {
        Destroy(gameObject);
    }
}
