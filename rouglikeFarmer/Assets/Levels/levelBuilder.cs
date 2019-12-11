using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelBuilder : MonoBehaviour
{
   public  GameObject[] FirstlevelLayouts;
   public  GameObject[] SecondlevelLayouts;
   private  GameObject levelLayout;
   private layoutBuilder _layoutBuilder;
   public  void BuildLevel(mainSceneController.Levels level)
   {
      switch (level)
      {
         case mainSceneController.Levels.FirstLevel:
            levelLayout = FirstlevelLayouts[Random.Range(0, FirstlevelLayouts.Length-1)];
            break;
         case mainSceneController.Levels.SecondLevel:
            levelLayout = SecondlevelLayouts[Random.Range(0, SecondlevelLayouts.Length-1)];
            break;
         default:
            levelLayout = FirstlevelLayouts[Random.Range(0, FirstlevelLayouts.Length-1)];
            break;
            
      }

      Instantiate(levelLayout, Vector3.zero, Quaternion.identity);
      _layoutBuilder = levelLayout.GetComponent<layoutBuilder>();
      _layoutBuilder.BuildLayout();

   }
}
