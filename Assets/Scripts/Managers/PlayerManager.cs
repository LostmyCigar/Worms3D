using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager _instance;  //In hindsight I feel that i should split this class
                                            //into a PlayerSpawnManager and a PlayerTurnManager but im prioritzing other things first
                                            //Could make PlayerSpawnManager and a PlayerTurnManager components used by this class aswell

    public delegate void StartTurnEvent(Transform player = null); //Is there a way to create events so that listeners doesnt have to take in the same parameters?
                                                                  //Example: HealthBar updates on StartPlayerTurn but has no need for the transform,
                                                                  //Does UpdateHealthBar() need to include a transform argument or is there a way to avoid this?
    public event StartTurnEvent OnStartTurn;
    public event Action OnEndTurn;

    [Header("Between Turns")]
    [Tooltip("This cannot be 0 since some weapons take time to shoot, also it would look bad")]
    [Range(1, 4)] public float _endTurnTime;
    public float _timeBetweenTurns;

    [Space]
    [Header("Player management")]
    public static List<Player> _activePlayers = new List<Player>();
    private static int _startPlayerCount = 0;
    public int _currentPlayerCount;

    [Space]
    [Header("Turn management")]
    public int _turnCount;  // These exist if we want to change things depending on how far into the game we are
    public int _roundCount; // For example: PickUp Spawnrate. Not used as of now
    public float _playerTurnTime;
    private float _playerTurnTimer;

    [SerializeField] private Material[] _playerMaterials;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private List<Transform> _spawnPoints;

    public static Player _currentPlayer; //Better to have this static or just let it be referenced through PlayerManager.GetInstance()? 
    private int _currentPlayerIndex;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);

        _startPlayerCount = 0;
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
        _currentPlayer.StartPlayerTurn();
        _currentPlayer.EnableWeapon();
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
            var playerObject = Instantiate(_playerPrefab, _spawnPoints[i].position, Quaternion.identity);
            playerObject.name = "Player " + (i + 1);
            playerObject.GetComponent<MeshRenderer>().material = _playerMaterials[i];
        }
    }

    #endregion

    #region Player turns 
    public void EndTurn()
    {
        StartCoroutine(EndturnWithWaitTime());


        IEnumerator EndturnWithWaitTime()
        {
            _currentPlayer.EndPlayerTurn();
            yield return new WaitForSeconds(_endTurnTime);
            _currentPlayer.DisableWeapon();
            OnEndTurn?.Invoke();

            _currentPlayerIndex++;
            _currentPlayerIndex %= _currentPlayerCount;
            _currentPlayer = _activePlayers[_currentPlayerIndex];

            _turnCount++;
            if (_turnCount % _currentPlayerCount == 0)  _roundCount++;

            yield return new WaitForSeconds(_timeBetweenTurns);
            OnStartTurn?.Invoke(_currentPlayer.transform);
            _currentPlayer.StartPlayerTurn();
            _currentPlayer.EnableWeapon();
        }    
    }

    #endregion
}
