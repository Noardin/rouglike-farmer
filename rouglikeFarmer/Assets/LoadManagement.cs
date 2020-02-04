using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadManagement : MonoBehaviour
{
    public player player;
    public levelBuilder LevelBuilder;
    public CameraFollow cameraController;

    private UnityAction GoToTrigger;
    
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
        player.healthManager.SetHearts();
        //checking wheter the player has save;
        if (playerdata == null)
        {
            player.healthManager.HealFull();
            SaveSystem.SavePlayer(player);
            
           
        }
        else
        {
            player.healthManager.HP = playerdata.HP;
        }
       
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
           Transform PlayerSpawn = GameObject.Find("PlayerSpawn").transform;
           position = PlayerSpawn.position;
        }
        

        player.transform.position = position;
        if (checkpointData == null)
        {
            
            Transform LevelStart = GameObject.Find("LevelStart").transform;

            GoToTrigger += () => {cameraController.Follow(player.transform);};
            Vector3 GoToPosition = LevelStart.position;
            Debug.Log("levelstart "+GoToPosition);
            player.GoTo(GoToPosition, 10, GoToTrigger);
            cameraController.LookAt(GoToPosition);
           
        }
       
        if (checkpointData != null)
        {
            Debug.Log("checkopoint != null");
            
            player.PlayerControlledMovementDisabled = false;
            cameraController.Follow(player.transform);
            if (Loader.LastSceneLoaded != Loader.Scene.MainMenu)
            {
                checkpointData.DeactivateCheckpoint();
            }
            
        }
        
    }
    

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
