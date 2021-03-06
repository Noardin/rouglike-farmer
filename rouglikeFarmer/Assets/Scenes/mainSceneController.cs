﻿using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
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
    public static Difficulty GameDifficulty;
    public static ScoreData ScoreData;
    public static int NewScore;
    public enum Levels{
        FirstLevel,
        SecondLevel,
        ThirdLevel
    }
    public enum Difficulty
    {
        Easy, Normal, Hard
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

    public static void GoToLevel(Levels level)
    {
        currentLvel = level;
        NextLvlSaveScene();
        Loader.Load(Loader.Scene.Main, Loader.Scene.Main);
    }
    public static void GoToFirstLevel()
    {
        SceneSeed = Random.Range(int.MinValue, int.MaxValue);
        currentLvel = Levels.FirstLevel;
    }
    public static void SaveScene()
    {
        if (!checkpointController.canRespawn())
        {
            GoToFirstLevel();
        }
        checkpointController.SaveCheckpoints();
        SceneData sceneData = new SceneData();
        SaveSystem.SaveSceneData(sceneData);
    }

    public static void NextLvlSaveScene()
    {
        checkpointController.ClearCheckpointsAndSave();
        SceneData sceneData = new SceneData();
        SaveSystem.SaveSceneData(sceneData);
        SaveSystem.SavePlayer(GameObject.Find("Player").GetComponent<player>());
    }

    public static void PrepareLvl()
    {
        SceneData sceneData = SaveSystem.LoadSceneData();
        Debug.Log("Player: "+ScoreData.PlayerName);

        if (sceneData != null)
        {
            SceneSeed = sceneData.seed;
            currentLvel = sceneData.currentLevel;
            GameDifficulty = sceneData.GameDifficulty;
            NewScore = sceneData.NewScoreData;
            GameObject.Find("_GM").GetComponent<GameManagement>().SetScoreCount(NewScore);

        }
        else
        {
            currentLvel = Levels.FirstLevel;
            SceneSeed = Random.seed;
            NewScore = 0;
        }
        
        
    }
    
}
