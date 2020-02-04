using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SceneData
{
    public int seed;
    public mainSceneController.Levels currentLevel;
    public mainSceneController.Difficulty GameDifficulty;

    public SceneData()
    {
        seed = mainSceneController.SceneSeed;
        currentLevel = mainSceneController.currentLvel;
        GameDifficulty = mainSceneController.GameDifficulty;
    }
}
