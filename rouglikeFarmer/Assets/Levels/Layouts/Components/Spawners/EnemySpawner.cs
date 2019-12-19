using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
   

    private void Start()
    {
        GameObject enemy = Enemies[Random.Range(0, Enemies.Length)];
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
