using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double HP;
    public PlayerData(player player)
    {
        HP = player.healthManager.HP;
    }
  
}
