using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpManager : MonoBehaviour
{
    public static PickUpManager _instance;

    [SerializeField] private Vector3 _spawnPositionCenter;
    [SerializeField] private Vector3 _spawnPositionArea;
    [SerializeField] private GameObject[] _pickUps = new GameObject[0];
    [SerializeField] [Range(0, 100)] private float _pickUpSpawnChance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(this);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PlayerManager.GetInstance().OnNewTurn += PickUpSpawnCheck;
    }
    public static PickUpManager GetInstance()
    {
        return _instance;
    }


    private void PickUpSpawnCheck()
    {
        var i = UnityEngine.Random.Range(0, 100);
        if (i < _pickUpSpawnChance)
        {
            PickUpSpawn();
        }
    }

    private void PickUpSpawn()
    {
        var i = UnityEngine.Random.Range(0, _pickUps.Length);
        Instantiate(_pickUps[i], RandomSpawnPosition, Quaternion.identity);
    }

    private Vector3 RandomSpawnPosition //I doubt this was the cleanest way of doing this
    {
        get
        {
            float xCenter = _spawnPositionCenter.x;
            float yCenter = _spawnPositionCenter.y;
            float zCenter = _spawnPositionCenter.z;

            float xPositionMin = xCenter - (_spawnPositionArea.x / 2);
            float xPositionMax = xCenter + (_spawnPositionArea.y / 2);

            float yPositionMin = yCenter - (_spawnPositionArea.y / 2);
            float yPositionMax = yCenter + (_spawnPositionArea.z / 2);

            float zPositionMin = zCenter - (_spawnPositionArea.z / 2);
            float zPositionMax = zCenter + (_spawnPositionArea.z / 2);

            float xPosition = UnityEngine.Random.Range(xPositionMin, xPositionMax);
            float yPosition = UnityEngine.Random.Range(yPositionMin, yPositionMax);
            float zPosition = UnityEngine.Random.Range(zPositionMin, zPositionMax);

            return new Vector3(xPosition, yPosition, zPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(_spawnPositionCenter, _spawnPositionArea);
    }
}
