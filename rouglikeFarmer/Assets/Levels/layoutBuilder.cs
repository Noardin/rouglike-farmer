using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class layoutBuilder : MonoBehaviour
{
   public Sprite SceneBackground;
   private SpriteRenderer SR;

   private void Awake()
   {
      GameObject SceneSR = GameObject.Find("SceneBackground");
      Debug.Log("scenebackground " + SceneSR);
      SR = SceneSR.GetComponent<SpriteRenderer>();
      SR.sprite = SceneBackground;

   }
   public void BuildLayout()
   {
      Debug.Log("buildLayout");
                                        
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.yellow;
      Gizmos.DrawRay(new Vector3(-35,-7.5f,0),new Vector3(200,0,0));
   }
}
