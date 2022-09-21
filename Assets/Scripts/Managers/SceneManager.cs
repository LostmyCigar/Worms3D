using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager _instance;
    public event Action PlaySceneLoad;
    public event Action MenuSceneLoad;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(this);


        UnityEngine.SceneManagement.SceneManager.sceneLoaded += NewSceneLoad;
    }
    public static SceneManager GetInstance()
    {
        return _instance;
    }

    #region Load new scene
    public void LoadPlayScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }

    public void LoadMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    #endregion

    #region OnSceneload
    private void NewSceneLoad(Scene scene, LoadSceneMode loadSceneMode) 
    {

        if (scene.name == "PlayScene")
        {
            PlaySceneLoad?.Invoke();
        }
        else if (scene.name == "MenuScene")
        {
            MenuSceneLoad?.Invoke();
        }
    }


    #endregion

}
