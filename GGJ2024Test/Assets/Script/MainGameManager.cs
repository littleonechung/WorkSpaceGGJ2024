using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum GameStatus
{
    GameInit,
    ShowQuestion,
    WaitAnser,
}



public class MainGameManager : MonoBehaviour
{
    public GameObject questionPanel;
    public GodManager godManager;

    private GodName currentGod;


    private void ChangeGameStatus(GameStatus status)
    {
        switch(status)
        {
            case GameStatus.GameInit:
                godManager.Setup(GodName.Default);
                break;
            case GameStatus.ShowQuestion:
                ShowQuestion();

                break;
            case GameStatus.WaitAnser:
                break;
            
            default: break;
        }
    }

    private void ShowQuestion()
    {
        questionPanel.SetActive(true);

    }

    private void OnClickAnser(GodFeeling feeling)
    {
        questionPanel.SetActive(false);
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
