using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
   public GameObject[] Platforms;
   

   private void Start()
   {
      GameObject platform = Platforms[Random.Range(0, Platforms.Length)];
      Instantiate(platform, transform.position, Quaternion.identity);
   }
}
