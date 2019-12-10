using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class checkpointData
{
   public float[] position;
   public string Id;
   public bool isActive;
   public bool isSet;
   public checkpointData(checkpoint checkpoint)
   {
      isActive = checkpoint.isActive;
      isSet = checkpoint.isSet;
      Id = checkpoint.UniqueId.uniqueId;
      position = new[] {checkpoint.transform.position.x, checkpoint.transform.position.y, checkpoint.transform.position.z};
   }
}
