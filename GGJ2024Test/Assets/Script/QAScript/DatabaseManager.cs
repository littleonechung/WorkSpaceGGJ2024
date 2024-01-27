using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private QuestionDatabase _questionDatabase;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private List<TextMeshProUGUI> answers;

    private List<Question> currentQuestionList;
    private int currentQuestionIndex;

    public Action<Answer> onClickAnswerBtn;
    #region UI UPDATE METHODS
    private void UpdateText(Question input)
    {
        question.text = input.Content;
        foreach(var answer in answers)
        {
            answer.gameObject.SetActive(false); 
        }
        for (int i = 0; i < input.Answers.Count; i++) 
        {
            answers[i].gameObject.SetActive(true);
            answers[i].text = input.Answers[i].Content;
            if (i >= 3)
                break;
        }
    }
    #endregion

    #region GENERAL METHODS
    public void SetQuestionList(GodName godName)
    {
        currentQuestionList = GetQuestionListViaGodName(godName);
    }

    public void UpdateQuestion(bool isFirst = false)
    {
        if (isFirst)  
            currentQuestionIndex = 0; 
        else 
            currentQuestionIndex++;
        UpdateText( currentQuestionList[currentQuestionIndex]);
    }

    public List<Question> GetQuestionListViaGodName(GodName godName)
    {
        foreach (QuestionDictItem questionDictItem in _questionDatabase.script)
            if (questionDictItem.Key == godName)
                return questionDictItem.Questions;
        return null;
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

    public void OnAnserClick(int answerIndex)
    {
        onClickAnswerBtn?.Invoke(currentQuestionList[currentQuestionIndex].Answers[answerIndex]);
    }
}
