using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] [Range(0.01f, 0.5f)] private float _growSpeed;
    [SerializeField] private TextMeshProUGUI text;

    private bool _growing;
    private void FixedUpdate()
    {
        if (text.fontSize <= _minSize)
        {
            _growing = true;
        } else if (text.fontSize >= _maxSize)
        {
            _growing = false;
        }

        if (_growing)
        {
            text.fontSize += _growSpeed;
        }
        else text.fontSize -= _growSpeed;
    }
}
