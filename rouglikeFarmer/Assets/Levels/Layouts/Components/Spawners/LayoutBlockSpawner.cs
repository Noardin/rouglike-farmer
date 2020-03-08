
using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Random = UnityEngine.Random;


public class LayoutBlockSpawner : MonoBehaviour
{
    public List<Blocks> LevelLayoutBlocks = new List<Blocks>();
    public enum BlockType
    {
        NormalBlocks,
        ShopBlocks,
        ChestBlocks,
        StartingBlocks,
        EndingBlocks,
        CheckpointBlocks
    }

    public BlockType CurrentBlockType;
    

    void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        Vector3 startingPoint = new Vector3(transform.position.x-10, transform.position.y-10,0);
        Vector3 endingPoint = new Vector3(transform.position.x+10, transform.position.y-10,0);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(startingPoint, endingPoint);
        
        #endif
    }
    //Test 
    private void Awake()
    {
        Blocks currentLevelBlocks = LevelLayoutBlocks[(int) mainSceneController.currentLvel];
        GameObject[] BlocksOfType = currentLevelBlocks.GetType().GetField(CurrentBlockType.ToString()).GetValue(currentLevelBlocks) as GameObject[];
       
        GameObject block = BlocksOfType[Random.Range(0, BlocksOfType.Length)];
        GameObject Instance = Instantiate(block, transform.position, Quaternion.identity);
        if (CurrentBlockType == BlockType.CheckpointBlocks)
        {
            Instance.GetComponentInChildren<checkpoint>().SetID(gameObject.GetComponent<UniqueId>());
        }
        Instance.transform.parent = transform.parent;
      
        Destroy(gameObject);
    }
}
[Serializable]
public class Blocks
{
    public GameObject[] NormalBlocks;
    public GameObject[] ShopBlocks;
    public GameObject[] ChestBlocks;
    public GameObject[] StartingBlocks;
    public GameObject[] EndingBlocks;
    public GameObject[] CheckpointBlocks;
}
#if UNITY_EDITOR
[CustomEditor(typeof(LayoutBlockSpawner))]
public class LayoutBlockSpawnerInspector : Editor
{
    private SerializedProperty _LevelBlocks;
    private SerializedProperty _BlockType;

    private void OnEnable()
    {
        _LevelBlocks = serializedObject.FindProperty("LevelLayoutBlocks");
        _BlockType = serializedObject.FindProperty("CurrentBlockType");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_BlockType, new GUIContent("Block Type"), false);
        
        _LevelBlocks.arraySize = Enum.GetNames(typeof(mainSceneController.Levels)).Length;
        for (var i = 0; i < Enum.GetNames(typeof(mainSceneController.Levels)).Length; i++)
        {
            var blocks = _LevelBlocks.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(blocks, new GUIContent(Enum.GetName(typeof(mainSceneController.Levels),i)), true);
        }

        serializedObject.ApplyModifiedProperties();

    }
}
#endif