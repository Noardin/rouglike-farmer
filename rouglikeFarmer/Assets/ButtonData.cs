using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    private MainMenu mainMenu;
    public ScoreData ScoreData;

    private void Start()
    {
        mainMenu = GameObject.Find("MenuPanel").GetComponent<MainMenu>();
    }

    public void onChoosePlayer()
    {
        mainMenu.ScoreData = ScoreData;
        GameObject.Find("Score").GetComponent<Text>().text = ScoreData.playerScore.scores[mainMenu.data.GameDifficulty].ToString();
    }
}
