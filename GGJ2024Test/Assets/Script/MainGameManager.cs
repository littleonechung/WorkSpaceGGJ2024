using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum GameStatus
{
    GameInit,
    ShowQuestion,
    WaitAnser,
    SwitchGod,
}



public class MainGameManager : MonoBehaviour
{
    public GameObject questionPanel;
    public DatabaseManager databaseManager;
    public GodManager godManager;

    private GodName currentGod;


    private void ChangeGameStatus(GameStatus status)
    {
        switch(status)
        {
            case GameStatus.GameInit:
                InitGame();              
                ChangeGameStatus(GameStatus.ShowQuestion);
                break;
            case GameStatus.SwitchGod:
                SwitchGod();
                godManager.Setup(currentGod);
                databaseManager.SetQuestionList(currentGod);
                break;
            case GameStatus.ShowQuestion:
                ShowQuestion();
                break;
            case GameStatus.WaitAnser:
                break;
            
            default: break;
        }
    }
    private void InitGame()
    {
        databaseManager.onClickAnswerBtn += OnClickAnser;
    }

    private void SwitchGod()
    {
        currentGod = GodName.Default;
    }

    private void ShowQuestion()
    {
        questionPanel.SetActive(true);
        databaseManager.UpdateQuestion();
    }

    private void OnClickAnser(Answer answer)
    {
        questionPanel.SetActive(false);
        GodFeeling feeling = answer.IsCorrect ? GodFeeling.Happy : GodFeeling.Angry;
        ShowFeedback(feeling);
    }

    private void ShowFeedback(GodFeeling feeling)
    {
        switch (feeling)
        {
            case GodFeeling.Angry:
                break;
            case GodFeeling.Happy:
                break;
            default: break;
        }
    }
   
}
