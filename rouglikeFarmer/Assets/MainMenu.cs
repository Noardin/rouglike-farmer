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
    public Animator animator;
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
        animator.SetTrigger("ButtonSlide");
    }

    public void NewEasyGame()
    {
        SceneData data = new SceneData();
        data.seed = Random.Range(Int32.MinValue, Int32.MaxValue);
        data.currentLevel = mainSceneController.Levels.FirstLevel;
        data.GameDifficulty = mainSceneController.Difficulty.Easy;
        SaveSystem.SaveSceneData(data);
        Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
    }
    public void NewNormalGame()
    {
        SceneData data = new SceneData();
        data.seed = Random.Range(Int32.MinValue, Int32.MaxValue);
        data.currentLevel = mainSceneController.Levels.FirstLevel;
        data.GameDifficulty = mainSceneController.Difficulty.Normal;
        SaveSystem.SaveSceneData(data);
        Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
    }
    public void NewHardyGame()
    {
        SceneData data = new SceneData();
        data.seed = Random.Range(Int32.MinValue, Int32.MaxValue);
        data.currentLevel = mainSceneController.Levels.FirstLevel;
        data.GameDifficulty = mainSceneController.Difficulty.Hard;
        SaveSystem.SaveSceneData(data);
        Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
    }

    public void Zpet()
    {
        animator.SetTrigger("ButtonSlideBack");
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
