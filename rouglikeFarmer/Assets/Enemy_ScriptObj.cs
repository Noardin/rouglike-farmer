using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy_ScriptObj : ScriptableObject 
{
    public new string name;
    public int health;
    
    public double Attack;
    public AnimatorController animController;
    

}
