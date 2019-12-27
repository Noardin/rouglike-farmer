using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    public GameObject checkpoint;
    [Range(0,100)]public float SpawnChance;

    private void Awake()
    {
        float randInt = Random.value;
        if (randInt > (1-SpawnChance/100f) )
        {
             Instantiate(checkpoint, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
       
    }
}
