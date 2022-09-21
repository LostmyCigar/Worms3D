using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private float _currentPlayerHp;


    private void Start()
    {
        UpdateCurrentPlayerHp();
        PlayerManager.GetInstance().OnNewTurn += UpdateCurrentPlayerHp;
    }

    private void UpdateCurrentPlayerHp()
    {
        _currentPlayerHp = PlayerManager._currentPlayer._health.Health;
        _healthBarFill.fillAmount = _currentPlayerHp / 100;
    }
}
