using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class mainSceneController
{
    private static int sceneSeed;
    public enum Levels{
        FirstLevel,
        SecondLevel
    }
    public static int SceneSeed
    {
        get { return sceneSeed; }
        set
        {
            Random.InitState(value);
            sceneSeed = value;
        }
    }

    public static void SaveScene()
    {
        SceneData sceneData = new SceneData(sceneSeed);
        SaveSystem.SaveSceneData(sceneData);
    }

    public static void PrepareLvl()
    {
        SceneData sceneData = SaveSystem.LoadSceneData();

        if (sceneData != null)
        {
            SceneSeed = sceneData.seed;
        }
        else
        {
            SceneSeed = Random.seed;
        }
        
    }
    
}
