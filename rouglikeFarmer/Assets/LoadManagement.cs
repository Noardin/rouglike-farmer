using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManagement : MonoBehaviour
{
    public player player;
    public levelBuilder LevelBuilder;
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerData playerdata = SaveSystem.LoadPlayer();

        mainSceneController.PrepareLvl();
        LevelBuilder.BuildLevel(mainSceneController.currentLvel);
        checkpointController.LoadCheckpoints(); 
        checkpoint checkpointData = checkpointController.LastCheckpoint;
        player.healthManager.HP = playerdata.HP;
        Vector3 position;
        if (checkpointData != null)
        {
            print("loading checkpoint");
            position.x = checkpointData.transform.position.x;
            position.y = checkpointData.transform.position.y;
            position.z = player.transform.position.z;
        }
        else
        {
            Transform LevelStart = GameObject.Find("LevelStart").transform;
            position = LevelStart.position;
        }
        

        player.transform.position = position;
        if (checkpointData != null & Loader.LastSceneLoaded != Loader.Scene.MainMenu)
        {
             checkpointData.DeactivateCheckpoint();
        }
       
        
        
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
