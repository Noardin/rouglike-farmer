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

    public static void addCheckpoint(checkpoint checkpoint)
    {
        activeCheckpoints.Add(checkpoint);
    }

    public static void LoadCheckpoints()
    {
        GameObject[] objects =GameObject.FindGameObjectsWithTag("checkpoint");
        foreach (var ob in objects)
        {
            AllCheckpoints.Add(ob.GetComponent<checkpoint>());
            Debug.Log("find"+ ob.GetComponent<checkpoint>().transform.position.x);

        }

        foreach (checkpoint cp in AllCheckpoints)
        {
            
            checkpointData data = SaveSystem.LoadCheckpoint(cp.Id);
            if (data == null)
            {
                SaveSystem.SaveCheckpoint(cp);
            }
            else
            {
                Debug.Log("data"+ data);
                cp.isActive = data.isActive;
                cp.isSet = data.isSet;
                if (cp.isActive)
                {
                    activeCheckpoints.Add(cp);
                }

                if (cp.isSet)
                {
                    LastCheckpoint = cp;
                }
            }
            
        }
    }

    public static void SaveCheckpoints()
    {
        foreach (var cp in AllCheckpoints)
        {
            Debug.Log("cp "+ cp);
        }
        SaveSystem.SaveCheckpoints(AllCheckpoints);
        
    }

    public static void SetCheckpoint(checkpoint checkpoint)
    {
        addCheckpoint(checkpoint);
        LastCheckpoint = checkpoint;
    }

    public static void UnsetLastCheckpoint()
    {
        Debug.Log("unset "+LastCheckpoint);
        if (LastCheckpoint != null)
        {
            removeCheckpoint(LastCheckpoint);
        }

        if (activeCheckpoints.Count > 0)
        {
            LastCheckpoint = activeCheckpoints.Last();
        }
        else
        {
            LastCheckpoint = null;
        }
        
        if (LastCheckpoint != null)
        {
            LastCheckpoint.SetCheckpoint();
        }
    }

    public static void removeCheckpoint(checkpoint checkpoint)
    {
        Debug.Log("remove");
        activeCheckpoints.Remove(checkpoint);
        AllCheckpoints.Remove(checkpoint);
    }
}
