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

        PlayerManager.GetInstance().OnStartTurn += UpdateText;
    }

    private void UpdateText(Transform transform)
    {
        _playerText.text = transform.name;
    }
}
