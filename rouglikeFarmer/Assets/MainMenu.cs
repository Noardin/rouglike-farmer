using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{
    public Text EnterbuttonText;
    public Animator animator;
    [HideInInspector]public SceneData data;
    public InputField PlayerName;
    [HideInInspector]public ScoreData ScoreData;
    public GameObject NewPlayerForm;
    public GameObject ExistingPlayerForm;
    public GameObject PlayerListObject;
    public RectTransform PlayerList;
    
    private void Start()
    {
        if (!SaveSystem.SavesExist())
        {
            EnterbuttonText.text = "New game";
        }
    }

    public void EnterGameButton()
    {
        if (SaveSystem.SavesExist())
        {
            Continue();
        }
        else
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        animator.SetTrigger("ButtonSlide");
    }

    public void NewEasyGame()
    {
        data.seed = Random.Range(Int32.MinValue, Int32.MaxValue);
        data.currentLevel = mainSceneController.Levels.FirstLevel;
        data.GameDifficulty = mainSceneController.Difficulty.Easy;
        
        animator.SetTrigger("SlideToPlayerSettings");
        
    }
    public void NewNormalGame()
    {
        data.seed = Random.Range(Int32.MinValue, Int32.MaxValue);
        data.currentLevel = mainSceneController.Levels.FirstLevel;
        data.GameDifficulty = mainSceneController.Difficulty.Normal;
       
        animator.SetTrigger("SlideToPlayerSettings");
    }
    public void NewHardyGame()
    {
        data.seed = Random.Range(Int32.MinValue, Int32.MaxValue);
        data.currentLevel = mainSceneController.Levels.FirstLevel;
        data.GameDifficulty = mainSceneController.Difficulty.Hard;
        
        animator.SetTrigger("SlideToPlayerSettings");
        
    }

    public void NewPlayer()
    {
        ExistingPlayerForm.SetActive(false);
        NewPlayerForm.SetActive(true);
        animator.SetTrigger("SlideToFinalStep");
    }

    public void ExistingPlayer()
    {
        NewPlayerForm.SetActive(false);
        ExistingPlayerForm.SetActive(true);
        List<ScoreData> players = SaveSystem.LoadScores();
        
        for (var i = 0; i < players.Count; i++)
        {
            GameObject item = Instantiate(PlayerListObject);
            
            item.transform.SetParent(PlayerList);
            item.transform.localScale = new Vector3(1,1,1);
            item.GetComponentInChildren<Text>().text = players[i].PlayerName;
            ButtonData itemData = item.GetComponent<ButtonData>();
            itemData.ScoreData = players[i];
        }
        animator.SetTrigger("SlideToFinalStep");
        

    }

    public void CreatePlayerAndPlay()
    {
        if (PlayerName.text != String.Empty)
        {
            ScoreData = new ScoreData(PlayerName.text, 0);
            int? PlayerID = SaveSystem.SaveNewScores(PlayerName.text);
            ScoreData.PlayerID = PlayerID;
            SaveSystem.SaveSceneData(data);
            mainSceneController.ScoreData = ScoreData;
            Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
        }
    }


    public void ChooseExistingPlay()
    {
        if (ScoreData != null)
        {
            SaveSystem.SaveScores(ScoreData);
            SaveSystem.SaveSceneData(data);
            mainSceneController.ScoreData = ScoreData;
            Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
        }
    }

    public void Zpet()
    {
        animator.SetTrigger("ButtonSlideBack");
    }

    public void ZpetFinalStep()
    {
        animator.SetTrigger("ButtonSlideBackFinalStep");
    }

    public void ZpetPlayerSettings()
    {
        animator.SetTrigger("ButtonSlideBackPlayerSettings");
    }
    private void Continue()
    {
        Loader.Load(Loader.Scene.MainMenu, Loader.Scene.Main);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
