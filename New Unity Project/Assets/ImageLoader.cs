using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;


public class ImageLoader : MonoBehaviour
{
   private string path;
   public string Filename = "newfile.png";
   public SpriteRenderer Image;
   public DropdownMaps dropdownMaps;
   public float minYSize;
   private void Start()
   {
      
   }

   public void OpenExplorer()
   {
      Cursor.visible = true;
      path = EditorUtility.OpenFilePanel("Overwrite with png", "", "*.png; *.jpeg; *.jpg");
      Cursor.visible = false;
      SaveImage(path, Filename);
   }

   public void EditImageName(string filename)
   {
      Debug.Log("f"+filename);
      Filename = filename;
   }

   void SaveImage(string path, string filename)
   {
      string FolderPath = Application.persistentDataPath + "/";
      string newImagePath = FolderPath + filename + ".png";
      if (path != null & System.IO.File.Exists(path))
      {
         int filecount = 0;
         while (System.IO.File.Exists(newImagePath))
         {
            filecount++;
            newImagePath = FolderPath + filename + filecount+".png";
         }

         if (filecount > 0)
         {
            filename += filecount.ToString();
         }
         
         System.IO.File.Copy(path, newImagePath);
         string mapsPath = Application.persistentDataPath + "/MapsImageData.txt";
         MapsImageData mapsImageData = new MapsImageData();
         mapsImageData.filename = filename;
         mapsImageData.filepath = newImagePath;
         string json = JsonUtility.ToJson(mapsImageData);
         using (StreamWriter w = File.AppendText(mapsPath))
         {
            w.WriteLine(json);
         }

         dropdownMaps.PopulateList();
         dropdownMaps.SetListTo(filename);
         
      }
   }

   public IEnumerator GetImage(string filename)
   {
      string FolderPath = Application.persistentDataPath + "/"+filename+".png";
      using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(FolderPath))
      {
         yield return uwr.SendWebRequest();

         if (uwr.isNetworkError || uwr.isHttpError)
         {
            Debug.Log(uwr.error);
         }
         else
         {
            // Get downloaded asset bundle
            var texture = DownloadHandlerTexture.GetContent(uwr);

            displayImage(texture);
         }
      }
      
   }

   public void displayImage(Texture2D t)
   {
      Sprite sprite = Sprite.Create(t,new Rect(0f,0f,t.width,t.height), new Vector2(0.5f,0.5f),100f );
      Image.sprite = sprite;
      float fact = minYSize / sprite.bounds.size.y;
      Image.transform.localScale = new Vector3(fact, fact, fact);
   }

   void Deletes()
   {
      string mapsPath = Application.persistentDataPath;
      DirectoryInfo di = new DirectoryInfo(mapsPath);
      foreach (var file in di.GetFiles())
      {
         file.Delete();
      }
     
   }

   public List<string> GetMapsList()
   {
      List<string> mapslist = new List<string>();
      string mapsPath = Application.persistentDataPath + "/MapsImageData.txt";
      using (StreamReader r = new StreamReader(mapsPath))
      {
         string line;
         while ((line = r.ReadLine()) != null)
         {
            MapsImageData data = JsonUtility.FromJson<MapsImageData>(line);
            mapslist.Add(data.filename);
         }
      }

      return mapslist;

   }
}

[Serializable]
public class MapsImageData
{
   public string filename;
   public string filepath;
}

