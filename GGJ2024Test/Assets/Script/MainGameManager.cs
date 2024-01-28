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
    End,

}

public class MainGameManager : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject infoPanel;
    public EndManager endManager;
    public CheckIfPlayed checkIfPlayed;
    public DatabaseManager databaseManager;
    public GodManager godManager;

    [SerializeField]
    GameObject titleCanvas = null;
    [SerializeField]
    GodName debugGod = GodName.Default;

    int currentGodIndex = 0;
    bool isFirstGame = false;
    private GodName currentGod;
    private List<GodName> currentGodList = new List<GodName>();
    [SerializeField]
    float infoTime = 5;

    private void Start()
    {
        ChangeGameStatus(GameStatus.GameInit);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            checkIfPlayed.PlayedBefore = true;
            checkIfPlayed.gameObject.SetActive(checkIfPlayed.PlayedBefore);
        }
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
                Observable.Timer(TimeSpan.FromSeconds(3f)).Subscribe(_ => { ChangeGameStatus(GameStatus.NextQuestion); }).AddTo(this);
                break;
            case GameStatus.NextQuestion:
                if(databaseManager.CheckNoQuestion())
                    ChangeGameStatus(GameStatus.SwitchGod);
                else
                    ShowQuestion();
                break;
            case GameStatus.End:
                endManager.SuccessEnd();
                StartCoroutine(ShowEnding());
                break;
            default: break;
        }
    }

    IEnumerator ShowEnding()
    {
        endManager.FadeIn();
        yield return new WaitForSeconds(4f);
        endManager.FadeOut();
        yield return new WaitForSeconds(1f);
        endManager.SetActive(false);
        ChangeGameStatus(GameStatus.GameInit);
    }

    private void InitGame()
    {
        if (!isFirstGame)
        {
            databaseManager.onClickAnswerBtn += OnClickAnser;
            isFirstGame = true;
        }
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

        checkIfPlayed.Check();
        currentGodIndex = 0;
        currentGod = GodName.Default;
    }

    private void SwitchGod()
    {
        if (debugGod != GodName.Default)
        {
            currentGod = debugGod;
        }
        else if (currentGod == GodName.EndGod)
        {
            ChangeGameStatus(GameStatus.End);
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
        SoundManager.Instance.PlayBGM(AudioName.stage_start,false);
    }

    IEnumerator DelayShowQuestion()
    {
        yield return new WaitForSeconds(infoTime);
        ChangeGameStatus(GameStatus.ShowQuestion);
    }

    private void ShowQuestion(bool first = false)
    {
        infoPanel.SetActive(false);
        godManager.ResetGodUI();
        questionPanel.SetActive(true);
        databaseManager.UpdateQuestion(first);
    }

    private void OnClickAnser(Answer answer)
    {
        questionPanel.SetActive(false);
        godManager.ShowResponse(answer);
        ChangeGameStatus(GameStatus.WaitShowResponse);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
