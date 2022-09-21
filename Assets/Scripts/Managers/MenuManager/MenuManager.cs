using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    /*
    private GameManager _gameManager;
    private void OnEnable()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    */
    public void PlayerAmountButton(int amount)
    {
        PlayerManager.GetInstance().SetStartPlayerCount(amount);
        SceneManager.GetInstance().LoadPlayScene();
    }

}
