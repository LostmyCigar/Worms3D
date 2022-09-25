using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.Timeline;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(CustomGravityObject))]
public class PickUp : MonoBehaviour
{
    protected CustomGravityObject _gravity;
    protected Collider _collider;

    protected bool _isFalling;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _gravity = GetComponent<CustomGravityObject>();
        _collider.isTrigger = true;
    }

    void Start()
    {
        StartCoroutine(FallAcceleration());
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
            CollidedWithPlayer();
        } else if (other.CompareTag("Ground"))
        {
            _gravity.gravityScale = 0;
            _isFalling = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var emptyPos = other.ClosestPointOnBounds(transform.position);
        var offset = (transform.position - emptyPos).normalized * 1.5f;

        transform.position = emptyPos + offset;
    }
    protected virtual void CollidedWithPlayer()
    {

    }
}
