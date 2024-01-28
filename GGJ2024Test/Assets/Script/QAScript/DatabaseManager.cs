using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UniRx;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private QuestionDatabase _questionDatabase;
    [SerializeField] private FadedText infoFadedText;
    [SerializeField] private FadedText question;
    [SerializeField] private List<FadedText> answers;
    [SerializeField] private List<GameObject> btns;

    private List<Question> currentQuestionList;
    private int currentQuestionIndex;
    string info;

    public Action<Answer> onClickAnswerBtn;
    #region UI UPDATE METHODS
    private void UpdateText(Question input)
    {
        foreach (var btn in btns)
        {
            btn.SetActive(false);
        }
        question.GenerateText(input.Content);
        Observable.Timer(System.TimeSpan.FromSeconds(question.GetFadeTime())).Subscribe(_ => {
            for (int i = 0; i < input.Answers.Count; i++)
            {
                btns[i].SetActive(true);
                answers[i].GenerateText(input.Answers[i].Content);
                if (i >= 3)
                    break;
            }
        }).AddTo(this);
    }
    private void UpdateInfoText(string text)
    {
        infoFadedText.GenerateText(text);
    }
    #endregion

    #region GENERAL METHODS
    public void SetQuestionList(GodName godName)
    {
        currentQuestionList = GetQuestionListViaGodName(godName);
    }

    public void SetInfo(GodName godName)
    {
        info = GetInfoViaGodName(godName);
    }

    public void UpdateInfo()
    {
        UpdateInfoText(info);
    }
    public void UpdateQuestion(bool isFirst)
    {
        if (isFirst)  
            currentQuestionIndex = 0; 
        else 
            currentQuestionIndex++;
        UpdateText( currentQuestionList[currentQuestionIndex]);
    }

    public bool CheckNoQuestion()
    {
        return currentQuestionIndex + 1 >= currentQuestionList.Count;
    }

    public List<Question> GetQuestionListViaGodName(GodName godName)
    {
        foreach (QuestionDictItem questionDictItem in _questionDatabase.script)
            if (questionDictItem.Key == godName)
                return questionDictItem.Questions;
        return null;
    }
    public string GetInfoViaGodName(GodName godName)
    {
        foreach (QuestionDictItem questionDictItem in _questionDatabase.script)
            if (questionDictItem.Key == godName)
                return questionDictItem.info;
        return "......";
    }
    public List<Question> GetRandomQuestionListViaGodName(GodName godName)
    {
        foreach (QuestionDictItem questionDictItem in _questionDatabase.script)
            if (questionDictItem.Key == godName)
                return Shuffle(questionDictItem.Questions);
        return null;
    }
    #endregion

    #region INNER METHODS
    private List<T> Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
        return list;
    }
    #endregion

    public void OnAnswerClick(int answerIndex)
    {
        onClickAnswerBtn?.Invoke(currentQuestionList[currentQuestionIndex].Answers[answerIndex]);
    }
}
