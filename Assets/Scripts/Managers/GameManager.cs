using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public List<GameObject> _weapons = new List<GameObject>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(this);
        DontDestroyOnLoad(this.gameObject);
    }
    public static GameManager GetInstance()
    {
        return _instance;
    }

}
