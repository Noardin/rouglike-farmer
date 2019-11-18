using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double HP;
    public float[] position;
    public PlayerData(player player)
    {
        HP = player.healthManager.HP;
        position = new[] {player.position.position.x, player.position.position.y, player.position.position.z};
    }
  
}
