using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager _instance;
    public static List<Player> _activePlayers = new List<Player>();

    public event Action OnNewTurn;

    private static int _startPlayerCount = 0;
    public int _currentPlayerCount;
    public int _turnCount;
    public int _roundCount;

    [SerializeField] private GameObject _player;
    [SerializeField] private List<Transform> _spawnPoints;

    public static Player _currentPlayer;
    private int _currentPlayerIndex;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(this);
    }

    private void Start()
    {
        SceneManager.GetInstance().PlaySceneLoad += InitializePlayers;
    }


    public static PlayerManager GetInstance()
    {
        return _instance;
    }

    #region Set and Spawn Players 

    public void SetStartPlayerCount(int amount)
    {
        _startPlayerCount = amount;
    }

    public void InitializePlayers()
    {
        _spawnPoints = GetSpawnPoints();
        SpawnPlayers();
        _currentPlayerIndex = 0;
        _currentPlayer = _activePlayers[_currentPlayerIndex];
        _currentPlayer.SetCurrentPlayer(true);
    }

    private List<Transform> GetSpawnPoints()
    {
        var spawnPointHolder = GameObject.FindGameObjectWithTag("SpawnPoint");
        var spawnPointsList = new List<Transform>(spawnPointHolder.GetComponentsInChildren<Transform>());
        spawnPointsList.Remove(spawnPointsList[0]); //Removes the parent transform that only exits to keep the hierarchy clean
        for (int i = 0; i < spawnPointsList.Count; i++) //Names objects for clarity in hierarchy
        {
            spawnPointsList[i].gameObject.name = "SpawnPoint: Player " + (i + 1);
        }

        return spawnPointsList;
    }

    private void SpawnPlayers()
    {
        _activePlayers.Clear();
        _currentPlayerCount = _startPlayerCount;
        for (int i = 0; i < _currentPlayerCount; i++)
        {
            var playerObject = Instantiate(_player, _spawnPoints[i].position, Quaternion.identity);
            playerObject.name = "Player " + (i + 1);
        }
    }

    #endregion

    public void EndTurn()
    {
        StartCoroutine(EndturnWithWaitTime());
        IEnumerator EndturnWithWaitTime()
        {
            _currentPlayer.SetCurrentPlayer(false);
            yield return new WaitForSeconds(2);

            _currentPlayerIndex++;
            _currentPlayerIndex %= _currentPlayerCount;
            _currentPlayer = _activePlayers[_currentPlayerIndex];

            _turnCount++;
            if (_turnCount % _currentPlayerCount == 0) { _roundCount++; }

            StartNewPlayerTurn();
        }    
    }

    private void StartNewPlayerTurn()
    {
        _currentPlayer.SetCurrentPlayer(true);
        OnNewTurn?.Invoke();
    }

}
