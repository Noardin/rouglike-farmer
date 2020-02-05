using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using Application = UnityEngine.Application;

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

   public static void ClearSave()
   {
      string CheckpointPath = Application.persistentDataPath + "/checkpoints";
      string PlayerPath = Application.persistentDataPath + "/player.mix";
      string ScenePath = Application.persistentDataPath + "/sceneData.mix";
      if (Directory.Exists(CheckpointPath))
      {
         foreach (var file in Directory.GetFiles(CheckpointPath) )
         {
            File.Delete(file);
         }
      }

      if (File.Exists(PlayerPath))
      {
         File.Delete(PlayerPath);
      }

      if (File.Exists(ScenePath))
      {
         File.Delete(ScenePath);
      }
   }

   public static checkpointData LoadCheckpoint(string Id)
   {
      string pathFolder = Application.persistentDataPath + "/checkpoints";
      string path = pathFolder + "/checkpoint_"+Id+".mix";
      if (File.Exists(path))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         try
         {
            checkpointData data = formatter.Deserialize(stream) as checkpointData;
            Debug.Log("loadData "+data.isSet);
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
      Debug.Log("isSet"+ checkpoint.isSet);
      checkpointData data = new checkpointData(checkpoint);
      string pathFolder = Application.persistentDataPath + "/checkpoints";
      if (!Directory.Exists(pathFolder))
      {
         Directory.CreateDirectory(pathFolder);
      }
      string path = pathFolder + "/checkpoint_"+data.Id+".mix";
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
      string pathFolder = Application.persistentDataPath + "/checkpoints";
      if (!Directory.Exists(pathFolder))
      {
         Directory.CreateDirectory(pathFolder);
      }
     
      foreach (checkpoint checkpoint in checkpoints)
      {
         Debug.Log("isSet"+ checkpoint.isSet);
         checkpointData data = new checkpointData(checkpoint);
         string path = pathFolder + "/checkpoint_"+data.Id+".mix";
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
