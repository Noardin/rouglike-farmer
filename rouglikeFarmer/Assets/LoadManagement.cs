﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManagement : MonoBehaviour
{
    public player player;
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerData playerdata = SaveSystem.LoadPlayer();
        checkpointController.LoadCheckpoints();
        if (checkpointController.CanRespawn)
        {
            checkpoint checkpointData = checkpointController.LastCheckpoint;
            mainSceneController.PrepareLvl();
            
            player.healthManager.HP = playerdata.HP;
            Vector3 position;
            position.x = checkpointData.transform.position.x;
            position.y = checkpointData.transform.position.y;
            position.z = player.transform.position.z;

            player.transform.position = position;
            checkpointData.DeactivateCheckpoint();
        }
        else
        {
            
        }
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
