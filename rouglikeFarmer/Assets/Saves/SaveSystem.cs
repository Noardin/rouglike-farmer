using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public class SaveSystem : MonoBehaviour
{
   public static void SavePlayer(player player)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/player.mix";
      FileStream stream = new FileStream(path, FileMode.Create);
      
      PlayerData data = new PlayerData(player);
      
      formatter.Serialize(stream, data);
      stream.Close();
   }

   public static checkpointData LoadCheckpoint(string Id)
   {
      string path = Application.persistentDataPath + "/checkpoint_"+Id+".mix";
      if (File.Exists(path))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         try
         {
            checkpointData data = formatter.Deserialize(stream) as checkpointData;
            return data;
         }
         catch
         {
            Debug.LogError("Save file not found in"+ path);
            return null;
         }
         finally
         {
            Debug.Log("closed");
             stream.Close();
         }
        
        
         
      }
      else
      {
         Debug.LogError("Save file not found in"+ path);
         return null;
      }
   }

   public static void SaveCheckpoint(checkpoint checkpoint)
   {
      
      BinaryFormatter formatter = new BinaryFormatter();
      checkpointData data = new checkpointData(checkpoint);
      string path = Application.persistentDataPath + "/checkpoint_"+data.Id+".mix";
      Debug.Log("seving path "+path);
      FileStream stream = new FileStream(path, FileMode.Create);
      try
      {
         formatter.Serialize(stream, data);
      }
      finally
      {
         stream.Close();
      }
   }

   public static void SaveCheckpoints(List<checkpoint> checkpoints)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      
     
      foreach (checkpoint checkpoint in checkpoints)
      {
         checkpointData data = new checkpointData(checkpoint);
         string path = Application.persistentDataPath + "/checkpoint_"+data.Id+".mix";
         FileStream stream = new FileStream(path, FileMode.Create);

         try
         {
            formatter.Serialize(stream, data);
         }
         finally
         {
            stream.Close();
         }
         
         
      }
   }

   public static void SaveSceneData(SceneData sceneData)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/sceneData.mix";
      Debug.Log("seving path "+path);
      FileStream stream = new FileStream(path, FileMode.Create);
      try
      {
         formatter.Serialize(stream, sceneData);
      }
      finally
      {
         stream.Close();
      }
   }

   public static bool SavesExist()
   {
      string path = Application.persistentDataPath + "/sceneData.mix";
      return File.Exists(path);
   }
   public static SceneData LoadSceneData()
   {
      string path = Application.persistentDataPath + "/sceneData.mix";
      if (File.Exists(path))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         try
         {
            SceneData data = formatter.Deserialize(stream) as SceneData;
            return data;
         }
         catch
         {
            return null;
         }
         finally
         {
             stream.Close();
             
         }
         
        
         
      }
      else
      {
         Debug.LogError("Save file not found in"+ path);
         return null;
      }
   }


   public static PlayerData LoadPlayer()
   {
      string path = Application.persistentDataPath + "/player.mix";
      if (File.Exists(path))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         
         PlayerData data = formatter.Deserialize(stream) as PlayerData;
         stream.Close();
         return data;
      }
      else
      {
         Debug.LogError("Save file not found in"+ path);
         return null;
      }
   }
}
