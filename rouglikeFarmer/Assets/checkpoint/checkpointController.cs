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
    public static bool CanRespawn;

    public static void addCheckpoint(checkpoint checkpoint)
    {
        activeCheckpoints.Add(checkpoint);
    }

    public static void LoadCheckpoints()
    {
        GameObject[] objects =GameObject.FindGameObjectsWithTag("checkpoint");
        AllCheckpoints = new List<checkpoint>();
        activeCheckpoints = new List<checkpoint>();
        foreach (var ob in objects)
        {
            AllCheckpoints.Add(ob.GetComponent<checkpoint>());

        }

        foreach (checkpoint cp in AllCheckpoints)
        {
            
            checkpointData data = SaveSystem.LoadCheckpoint(cp.UniqueId.uniqueId);
            if (data == null)
            {
                SaveSystem.SaveCheckpoint(cp);
            }
            else
            {
                Debug.Log("data.isSet: "+ data.isSet);
                cp.isActive = data.isActive;
                cp.isSet = data.isSet;
                if (cp.isActive)
                {
                    activeCheckpoints.Add(cp);
                }

                if (cp.isSet)
                {
                    CanRespawn = true;
                    LastCheckpoint = cp;
                }
            }
            
        }
    }

    public static void SaveCheckpoints()
    {
        SaveSystem.SaveCheckpoints(AllCheckpoints);
        
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
        Debug.Log("unset "+LastCheckpoint);
        LastCheckpoint.isActive = false;
        LastCheckpoint.isSet = false;
        removeCheckpoint(LastCheckpoint);
        

        if (activeCheckpoints.Count > 0)
        {
            Debug.Log("still othercheckpoints");
            LastCheckpoint = activeCheckpoints.Last();
            Debug.Log("next checkpoint "+ LastCheckpoint);
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
        Debug.Log("remove");
        activeCheckpoints.Remove(checkpoint);
        foreach (checkpoint cp in AllCheckpoints)
        {
            Debug.Log("allcheckpoints "+ cp);
        }
        

    }
}
