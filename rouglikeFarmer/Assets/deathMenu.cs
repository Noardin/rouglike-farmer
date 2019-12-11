using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathMenu : MonoBehaviour
{
    public Text LoadCheckpointText;
    
    public void quit()
    {
        Application.Quit();
    }

    public void LoadLastCheckpoint()
    {
        if (checkpointController.CanRespawn)
        {
            Loader.Load(Loader.Scene.Main);
        }
        else
        {
            Application.Quit();
        }
         
    }

    private void Awake()
    {
        if (LoadCheckpointText == null)
        {
            
        }
    }

    private void Start()
    {
        if (!checkpointController.CanRespawn)
        {
            LoadCheckpointText.text = "TRY AGAIN";
        }
    }
}
