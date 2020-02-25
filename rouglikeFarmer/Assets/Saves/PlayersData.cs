using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    public int PlayerID;
    public string PlayerName;
    public int PlayerScore;

    public ScoreData(int playerID, string playerName, int playerScore)
    {
        PlayerID = playerID;
        PlayerName = playerName;
        PlayerScore = playerScore;
    }
    
}
