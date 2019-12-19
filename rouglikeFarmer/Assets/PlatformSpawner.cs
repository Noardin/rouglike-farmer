using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
   public List<Platforms> LevelPlatforms = new List<Platforms>();

   private void Start()
   {
      Platforms currentLevelPlatforms = LevelPlatforms[(int) mainSceneController.currentLvel];
      GameObject platform = currentLevelPlatforms.platforms[Random.Range(0, currentLevelPlatforms.platforms.Length)];
      GameObject Instance = Instantiate(platform, transform.position, Quaternion.identity);
      Instance.transform.parent = transform.parent;
   }
}
[Serializable]
public class Platforms
{
   public GameObject[] platforms;
}
[CustomEditor(typeof(PlatformSpawner))]
public class PlatformSpawnerInspector : Editor
{
   private SerializedProperty _LevelPlatforms;

   private void OnEnable()
   {
      _LevelPlatforms = serializedObject.FindProperty("LevelPlatforms");
   }
   public override void OnInspectorGUI()
   {
      serializedObject.Update();

      _LevelPlatforms.arraySize = Enum.GetNames(typeof(mainSceneController.Levels)).Length;
      for (var i = 0; i < Enum.GetNames(typeof(mainSceneController.Levels)).Length; i++)
      {
         var platforms = _LevelPlatforms.GetArrayElementAtIndex(i);
         EditorGUILayout.PropertyField(platforms, new GUIContent(Enum.GetName(typeof(mainSceneController.Levels),i)), true);
      }

      serializedObject.ApplyModifiedProperties();

   }
}
