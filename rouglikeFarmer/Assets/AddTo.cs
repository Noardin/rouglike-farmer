using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddTo : MonoBehaviour
{
    public enum Grounds
    {
        MidGround,
        BackGround,
        ForeGround
    }

    public Grounds AddToGround;

    private void Start()
    {
        GameObject SentTo = GameObject.FindWithTag(AddToGround.ToString());
        transform.parent = SentTo.transform;
    }
}
