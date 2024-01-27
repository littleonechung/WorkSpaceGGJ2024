using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using static UnityEngine.ParticleSystem;

public enum GameStatus
{
    GameInit,
    SwitchGod,
    ShowQuestion,
    WaitShowResponse,
    NextQuestion,

}

public class MainGameManager : MonoBehaviour
{
    public GameObject questionPanel;
    public DatabaseManager databaseManager;
    public GodManager godManager;

    private GodName currentGod;

    private void Start()
    {
        ChangeGameStatus(GameStatus.GameInit);
    }
    private void ChangeGameStatus(GameStatus status)
    {
        switch(status)
        {
            case GameStatus.GameInit:
                InitGame();              
                ChangeGameStatus(GameStatus.SwitchGod);
                break;
            case GameStatus.SwitchGod:
                SwitchGod();
                godManager.Setup(currentGod);
                databaseManager.SetQuestionList(currentGod);
                ChangeGameStatus(GameStatus.ShowQuestion);
                break;
            case GameStatus.ShowQuestion:
                ShowQuestion(true);
                break;
            case GameStatus.WaitShowResponse:
                Observable.Timer(System.TimeSpan.FromSeconds(2f)).Subscribe(_ => { ChangeGameStatus(GameStatus.NextQuestion); }).AddTo(this);
                break;
            case GameStatus.NextQuestion:
                if(databaseManager.CheckNoQuestion())
                    ChangeGameStatus(GameStatus.SwitchGod);
                else
                    ShowQuestion();
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
        currentGod = GodName.Tutorial;
    }

    private void ShowQuestion(bool first = false)
    {
        questionPanel.SetActive(true);
        godManager.ResetGodUI();
        databaseManager.UpdateQuestion(first);
    }

    private void OnClickAnser(Answer answer)
    {
        questionPanel.SetActive(false);
        godManager.ShowResponse(answer);
        ChangeGameStatus(GameStatus.WaitShowResponse);
    }
   
}
