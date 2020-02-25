[System.Serializable]
public class ScoreData
{
    public int? PlayerID = null;
    public string PlayerName;
    public int PlayerScore;

    public ScoreData(string playerName, int playerScore)
    {
        PlayerName = playerName;
        PlayerScore = playerScore;
    }
    
}
