using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{
    public Text EnterbuttonText;
    private void Start()
    {
        if (!SaveSystem.SavesExist())
        {
            EnterbuttonText.text = "New game";
        }
    }

    public void EnterGameButton()
    {
        if (SaveSystem.SavesExist())
        {
            Continue();
        }
        else
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SceneData data = new SceneData();
        data.seed = Random.Range(Int32.MinValue, Int32.MaxValue);
        data.currentLevel = mainSceneController.Levels.FirstLevel;
        SaveSystem.SaveSceneData(data);
        Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
    }
    private void Continue()
    {
        Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
