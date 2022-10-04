using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private float _currentPlayerHp;


    private void Start()
    {
        UpdateHealthBar(null);
        PlayerManager.GetInstance().OnStartTurn += UpdateHealthBar;
    }

    private void UpdateHealthBar(Transform transform) //Feels weird to include the transform here just to be able to listen to the StartTurnEvent
    {
        _currentPlayerHp = PlayerManager._currentPlayer._health.Health;
        _healthBarFill.fillAmount = _currentPlayerHp / 100;
    }
}
