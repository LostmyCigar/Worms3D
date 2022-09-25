using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform _shootPoint;
    private Camera _cam;
    [SerializeField] protected Vector3 _aimOffset = new Vector3(0f, 0.4f, 0f);
    private LineRenderer _aimLine;
    public virtual void Shoot() { }


    private void Awake()
    {
        _cam = Camera.main;
        _aimLine = GetComponent<LineRenderer>();
        _aimLine.useWorldSpace = false;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_cam.transform.forward + _aimOffset);
    }

    private void SetAimLine()
    {
    
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

}
