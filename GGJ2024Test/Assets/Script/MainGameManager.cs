using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using static UnityEngine.ParticleSystem;

public enum GameStatus
{
    GameInit,
    SwitchGod,
    ShowInfo,
    ShowQuestion,
    WaitShowResponse,
    NextQuestion,

}

public class MainGameManager : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject infoPanel;
    public DatabaseManager databaseManager;
    public GodManager godManager;

    [SerializeField]
    GameObject titleCanvas = null;
    [SerializeField]
    GodName debugGod = GodName.Default;

    int currentGodIndex = 0;
    private GodName currentGod;
    private List<GodName> currentGodList = new List<GodName>();
    [SerializeField]
    float infoTime = 5;

    private void Start()
    {
        ChangeGameStatus(GameStatus.GameInit);
    }

    public void StartGame()
    {
        ChangeGameStatus(GameStatus.SwitchGod);
    }

    private void ChangeGameStatus(GameStatus status)
    {
        switch(status)
        {
            case GameStatus.GameInit:
                InitGame();              
                break;
            case GameStatus.SwitchGod:
                titleCanvas.SetActive(false);
                SwitchGod();
                godManager.Setup(currentGod);
                databaseManager.SetQuestionList(currentGod);
                databaseManager.SetInfo(currentGod);
                ChangeGameStatus(GameStatus.ShowInfo);
                break;
            case GameStatus.ShowInfo:
                ShowInfo();
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
        titleCanvas.gameObject.SetActive(true);
        currentGodList.Clear();
        GodName[] enumValues = (GodName[])Enum.GetValues(typeof(GodName));
        foreach (GodName godName in enumValues)
        {
            if (godName != GodName.Default)
            {
                currentGodList.Add(godName);
            }
        }
    }

    private void SwitchGod()
    {
        if (debugGod != GodName.Default)
        {
            currentGod = debugGod;
        }
        else
        {
            currentGod = currentGodList[currentGodIndex];
            currentGodIndex++;
        }
    }

    private void ShowInfo()
    {
        infoPanel.SetActive(true);
        questionPanel.SetActive(false);
        godManager.ResetGodUI();
        databaseManager.UpdateInfo();
        StartCoroutine(DelayShowQuestion());
    }

    IEnumerator DelayShowQuestion()
    {
        yield return new WaitForSeconds(infoTime);
        ChangeGameStatus(GameStatus.ShowQuestion);
    }

    private void ShowQuestion(bool first = false)
    {
        infoPanel.SetActive(false);
        questionPanel.SetActive(true);
        databaseManager.UpdateQuestion(first);
    }

    private void OnClickAnser(Answer answer)
    {
        questionPanel.SetActive(false);
        godManager.ShowResponse(answer);
        ChangeGameStatus(GameStatus.WaitShowResponse);
    }
   
}
