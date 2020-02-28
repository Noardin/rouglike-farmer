using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class SceneData
{
    public int seed;
    public ScoreData NewScoreData;
    public mainSceneController.Levels currentLevel;
    public mainSceneController.Difficulty GameDifficulty;

    public SceneData()
    {
        seed = mainSceneController.SceneSeed;
        currentLevel = mainSceneController.currentLvel;
        GameDifficulty = mainSceneController.GameDifficulty;
        NewScoreData = mainSceneController.NewScore;
    }
}
