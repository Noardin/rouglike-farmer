using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{

    public GameObject Sheep;

    public Transform SheepSpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Sheep, SheepSpawnPos.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
