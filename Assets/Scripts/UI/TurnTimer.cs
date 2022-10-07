using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _turnTimerText;
    private Image _turnTimerImage;
    void Start()
    {
        _turnTimerImage = GetComponent<Image>();
    }

    void Update()
    {
        float maxTurnTime = PlayerManager.GetInstance()._playerTurnTimeMax;
        float turnTimeLeft = PlayerManager.GetInstance()._playerTurnTimer;

        if (turnTimeLeft > maxTurnTime)
        {
            turnTimeLeft = maxTurnTime;
        } else if(turnTimeLeft < 0)
        {
            turnTimeLeft = 0;
        }

        _turnTimerText.text = Mathf.RoundToInt(turnTimeLeft).ToString();

        _turnTimerImage.fillAmount = turnTimeLeft/ maxTurnTime;
    }
}
