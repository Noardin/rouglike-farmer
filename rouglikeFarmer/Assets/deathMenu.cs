using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathMenu : MonoBehaviour
{
    public Text LoadCheckpointText;
    
    public void quit()
    {
        SaveSystem.ClearSave();
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
        }
    }
}
