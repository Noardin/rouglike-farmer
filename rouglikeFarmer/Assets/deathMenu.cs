using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathMenu : MonoBehaviour
{
    public Text LoadCheckpointText;
    public Text ScoreNumber;
    public GameObject Score;
    public GameObject buttons;
    
    public void quit()
    {
        Application.Quit();
    }

    public void LoadLastCheckpoint()
    {
        Loader.Load(Loader.Scene.GameOver, Loader.Scene.Main);
         
    }

    public void MainMenu()
    {
        SaveSystem.ClearSave();
        Loader.Load(Loader.Scene.GameOver, Loader.Scene.MainMenu);
    }


    private void Start()
    {
        if (!checkpointController.CanRespawn)
        {
            LoadCheckpointText.text = "TRY AGAIN";
            SaveSystem.ClearSave();
        }

        ScoreNumber.text = mainSceneController.NewScore.ToString();
        if (mainSceneController.ScoreData.playerScore.scores[mainSceneController.GameDifficulty] < mainSceneController.NewScore)
        {
            mainSceneController.ScoreData.playerScore.scores[mainSceneController.GameDifficulty] = mainSceneController.NewScore;
            SaveSystem.SaveScores(mainSceneController.ScoreData);
        }

    }

    private void Update()
    {
        if ((Input.touchCount > 0 | Input.anyKey) & !buttons.activeSelf)
        {
            buttons.SetActive(true);
            Score.SetActive(false);
        }
    }
}
