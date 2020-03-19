using System.Collections.Generic;

[System.Serializable]
public class ScoreData
{
    public int? PlayerID = null;
    public string PlayerName;

    public PlayerScore playerScore;

    public ScoreData(string playerName,  PlayerScore playerScore)
    {
        PlayerName = playerName;
        this.playerScore = playerScore;
    }
[System.Serializable]
    public class PlayerScore
    {
        public Dictionary<mainSceneController.Difficulty, int> scores =
        
            new Dictionary<mainSceneController.Difficulty, int>()
            {
                {mainSceneController.Difficulty.Easy, 0},
                {mainSceneController.Difficulty.Normal,0},
                {mainSceneController.Difficulty.Hard,0}
            };
    }
}
    

