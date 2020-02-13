using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public player player;
    public GameObject pauseMenuUI;
    // Update is called once per frame

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void GiveUp()
    {
        paused = false;
        checkpointController.ClearCheckpointsAndSave();
        Time.timeScale = 1f;
        player.healthManager.Die();
    }
    public void Exit()
    {
        paused = false;
        checkpointController.SaveCheckpoints();
        Loader.Load(Loader.Scene.Main, Loader.Scene.MainMenu);
        Time.timeScale = 1f;

    }

}
