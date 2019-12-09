using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathMenu : MonoBehaviour {
    public void quit()
    {
        Application.Quit();
    }

    public void LoadLastCheckpoint()
    {
        Loader.Load(Loader.Scene.Main);
    }
    
}
