using System.IO;
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
