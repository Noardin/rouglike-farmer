using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveLevel : MonoBehaviour
{
    public mainSceneController.Levels ToLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            GoToLVL();
        }
    }

    public void GoToLVL()
    {
        mainSceneController.GoToLevel(ToLevel);
    }
}
