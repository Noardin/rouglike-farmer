using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class checkpointData
{

   public string Id;
   public bool isActive;
   public bool isSet;
   public bool isDisabled;
   public checkpointData(checkpoint checkpoint)
   {
      isActive = checkpoint.isActive;
      isSet = checkpoint.isSet;
      Id = checkpoint.UniqueId.uniqueId;
      isDisabled = checkpoint.isDisabled;
      
   }
}
