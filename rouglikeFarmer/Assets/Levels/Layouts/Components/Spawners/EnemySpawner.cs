using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    private GameManagement GM;

    public enum EnemyType
    {
        Mrekv,
        Ovce,
        Cow,
        Svestka,
        Random
    }

    public EnemyType enemyType;
   

    private void Start()
    {
        GM = GameObject.Find("_GM").GetComponent<GameManagement>();
        if (enemyType == EnemyType.Random)
        {
            GameObject enemy = Enemies[Random.Range(0, Enemies.Length)];
            Instantiate(enemy, transform.position, Quaternion.identity);   
        }
        else
        {
            GameObject enemy = Enemies[enemyType.GetHashCode()];
            Instantiate(enemy, transform.position, Quaternion.identity);
        }

        GM.Max++;

    }
}
