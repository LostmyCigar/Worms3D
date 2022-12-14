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

    public delegate void PickUpSpawnEvent(Transform pickUp);
    public event PickUpSpawnEvent OnPickUpSpawn;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PlayerManager.GetInstance().OnEndTurn += PickUpSpawnCheck;
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

    public void PickUpSpawn()
    {
        var i = UnityEngine.Random.Range(0, _pickUps.Length);
        var pickUp = Instantiate(_pickUps[i], RandomSpawnPosition, _pickUps[i].transform.rotation);
        OnPickUpSpawn?.Invoke(pickUp.transform);
    }

    private Vector3 RandomSpawnPosition //I doubt this was the cleanest way of doing this
    {
        get
        {
            float xCenter = _spawnPositionCenter.x;
            float yCenter = _spawnPositionCenter.y;
            float zCenter = _spawnPositionCenter.z;

            float xPositionMin = xCenter - (_spawnPositionArea.x / 2);
            float xPositionMax = xCenter + (_spawnPositionArea.x / 2);

            float yPositionMin = yCenter - (_spawnPositionArea.y / 2);
            float yPositionMax = yCenter + (_spawnPositionArea.y / 2);

            float zPositionMin = zCenter - (_spawnPositionArea.z / 2);
            float zPositionMax = zCenter + (_spawnPositionArea.z / 2);

            int xPosition = Mathf.RoundToInt(UnityEngine.Random.Range(xPositionMin, xPositionMax));
            int yPosition = Mathf.RoundToInt(UnityEngine.Random.Range(yPositionMin, yPositionMax));
            int zPosition = Mathf.RoundToInt(UnityEngine.Random.Range(zPositionMin, zPositionMax));

            return new Vector3(xPosition, yPosition, zPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(_spawnPositionCenter, _spawnPositionArea);
    }
}
