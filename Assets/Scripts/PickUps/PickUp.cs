using System.Collections;
using System.Collections.Generic;
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

    protected virtual void CollidedWithPlayer()
    {

    }
}
