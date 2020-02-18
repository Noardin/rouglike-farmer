using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Xml.Schema;
using UnityEngine;

public static class checkpointController
{
    public static List<checkpoint> activeCheckpoints = new List<checkpoint>();
    public static List<checkpoint> AllCheckpoints = new List<checkpoint>();
    public static checkpoint LastCheckpoint;
    public static GameObject CheckpointFolder;
    public static bool CanRespawn;

    public static void addCheckpoint(checkpoint checkpoint)
    {
        activeCheckpoints.Add(checkpoint);
    }

    public static void LoadCheckpoints()
    {
        GameObject[] objects =GameObject.FindGameObjectsWithTag("checkpoint");
        Debug.Log("objects "+objects.Length);
        CheckpointFolder = GameObject.Find("Checkpoints");
        AllCheckpoints = new List<checkpoint>();
        activeCheckpoints = new List<checkpoint>();
        foreach (var ob in objects)
        {
            AllCheckpoints.Add(ob.GetComponent<checkpoint>());

        }
        Debug.Log("allCheckpoints "+AllCheckpoints.Count);
        foreach (checkpoint cp in AllCheckpoints)
        {
            cp.transform.parent = CheckpointFolder.transform;
            checkpointData data = SaveSystem.LoadCheckpoint(cp.UniqueId.uniqueId);
            if (data == null)
            {
                Debug.Log("newCheckpoints");
                SaveSystem.SaveCheckpoint(cp);
            }
            else
            {
                if (!data.isDisabled)
                {
                    cp.isActive = data.isActive;
                    cp.isSet = data.isSet;
                    if (cp.isActive)
                    {
                        activeCheckpoints.Add(cp);
                        cp.SR.sprite = cp.AciteCheckpointSprite;
                        cp.ParticleSystem.Play();
                    }
    
                    if (cp.isSet)
                    {
                        CanRespawn = true;
                        LastCheckpoint = cp;
                    }
                }
                else
                {
                    cp.isDisabled = data.isDisabled;
                }
                
            }
            
        }

    }

    public static void SaveCheckpoints()
    {
        SaveSystem.SaveCheckpoints(AllCheckpoints);
        
    }

    public static void ClearCheckpointsAndSave()
    {
        foreach (var checkpoint in activeCheckpoints)
        {
            checkpoint.isActive = false;
            checkpoint.isSet = false;
            checkpoint.isDisabled = false;
        }

        CanRespawn = false;
        SaveSystem.SaveCheckpoints(AllCheckpoints);
    }

    public static bool canRespawn()
    {
        foreach (checkpoint cp in activeCheckpoints)
        {

            if (cp.isSet)
            {
                return true;
            }
        }

        return false;
    }

    public static void SetCheckpoint(checkpoint checkpoint)
    {
        Debug.Log("set new checkpoint "+checkpoint);
        
        LastCheckpoint = checkpoint;
        checkpoint.isSet = true;
        CanRespawn = true;
    }

    public static void AcitivateCheckpoint(checkpoint checkpoint)
    {
        addCheckpoint(checkpoint);
        checkpoint.isActive = true;
    }

    public static void SetCheckpointAndUnsetLast(checkpoint checkpoint)
    {
        if (LastCheckpoint != null)
        {
            removeCheckpoint(LastCheckpoint);
            LastCheckpoint.isSet = false;
        }
        
        SetCheckpoint(checkpoint);
        
    }

    public static void DeactivateLastCheckpoint()
    {
        LastCheckpoint.isActive = false;
        LastCheckpoint.isSet = false;

        removeCheckpoint(LastCheckpoint);
        
        if (activeCheckpoints.Count > 0)
        {
            LastCheckpoint = activeCheckpoints.Last();
            LastCheckpoint.SetCheckpoint();
        }
        else
        {
            
            LastCheckpoint = null;
            CanRespawn = false;
        }
        
    }

    public static void removeCheckpoint(checkpoint checkpoint)
    {
        activeCheckpoints.Remove(checkpoint);
        LastCheckpoint.isDisabled = true;

    }
}
