using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{

    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length==j) ? Arr[0] : Arr[j];            
    }
}
public static class mainSceneController
{
    private static int sceneSeed;
    public static Levels currentLvel;
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

    public static void GoToNextLevel()
    {
        currentLvel = currentLvel.Next();
    } 
    public static void SaveScene()
    {
        SceneData sceneData = new SceneData();
        SaveSystem.SaveSceneData(sceneData);
    }

    public static void PrepareLvl()
    {
        SceneData sceneData = SaveSystem.LoadSceneData();

        if (sceneData != null)
        {
            SceneSeed = sceneData.seed;
            currentLvel = sceneData.currentLevel;
            
        }
        else
        {
            currentLvel = Levels.FirstLevel;
            SceneSeed = Random.seed;
        }
        
        
    }
    
}
