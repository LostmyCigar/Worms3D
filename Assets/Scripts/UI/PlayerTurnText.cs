using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnText : MonoBehaviour
{
    private TextMeshProUGUI _playerText;
    void Start()
    {
        _playerText = GetComponent<TextMeshProUGUI>();

        PlayerManager.GetInstance().OnNewTurn += UpdateText;
    }

    private void UpdateText()
    {
        _playerText.text = PlayerManager._currentPlayer.gameObject.name;
    }
}
