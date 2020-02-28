using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public Text MinText;
    public Text MaxText;
    private int max;
    private int min;
    public Text Score;
    public Animator killcountanim;
    public int Max
    {
        get { return max;}
        set
        {
            MaxText.text = value.ToString();
            max = value;
        }
    }

    public int Min
    {
        get { return min;}
        set
        {
            MinText.text = value.ToString();
            if (value >= Max)
            {
                MinText.color = Color.green;
            }
            else
            {
                MinText.color = Color.red;
            }
            min = value;
        }
    }

    public void KillCountUp()
    {
        Min += 1;
        
    }

    public void SetScoreCount(int Score)
    {
        this.Score.text = Score.ToString();
    }

    private void Start()
    {
        Min = 0;
        MinText.color = Color.red;
    }

    public bool CanFinish()
    {
        if (min >= max)
        {
            return true;
        }
        killcountanim.SetTrigger("Reminder");
        return false;
    }
}
