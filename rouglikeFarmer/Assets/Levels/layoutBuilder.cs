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
}
