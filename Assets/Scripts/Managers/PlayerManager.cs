using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager _instance;
    public event Action OnNewTurn;

    #region Player count/current Player

    private static int _startPlayerCount = 0;
    public int _currentPlayerCount;


    [SerializeField] private GameObject _player;
    [SerializeField] private List<Player> _players = new List<Player>();
    [SerializeField] private List<Transform> _spawnPoints;

    public static Player _currentPlayer;
    private int _currentPlayerIndex;

    #endregion

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
        _currentPlayer = _players[_currentPlayerIndex];
        _currentPlayer.EnablePlayer();

        for (int i = 0; i < _players.Count; i++)
        {
            _players[i]._health.OnPlayerDeath += OnPlayerDeath;
        }

        Debug.Log(_startPlayerCount);
        Debug.Log(_players.Count);
        Debug.Log(_currentPlayer);

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
        _players.Clear();
        _currentPlayerCount = _startPlayerCount;
        for (int i = 0; i < _currentPlayerCount; i++)
        {
            var playerObject = Instantiate(_player, _spawnPoints[i].position, Quaternion.identity);
            playerObject.name = "Player " + (i + 1);
            AddPlayerToList(playerObject);
        }
    }

    private void AddPlayerToList(GameObject playerObject)
    {
       _players.Add(playerObject.GetComponent<Player>());
    }

    #endregion

    public void EndTurn()
    {

        StartCoroutine(EndturnWithWaitTime());


        IEnumerator EndturnWithWaitTime()
        {
            _currentPlayer.DisablePlayer();
            yield return new WaitForSeconds(2);
            _currentPlayerIndex++;
            _currentPlayerIndex %= _currentPlayerCount;
            _currentPlayer = _players[_currentPlayerIndex];
            Debug.Log(_currentPlayer.gameObject.name);
            StartNewPlayerTurn();
        }    
    }

    private void StartNewPlayerTurn()
    {
        _currentPlayer.EnablePlayer();
        OnNewTurn?.Invoke();
    }

    private void OnPlayerDeath(Player player)
    {
        _players.Remove(player);
        player.Die();
    }
}
