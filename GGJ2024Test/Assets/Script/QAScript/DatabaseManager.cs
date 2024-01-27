using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private QuestionDatabase _questionDatabase;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private List<TextMeshProUGUI> answers;

    #region UI UPDATE METHODS
    public void UpdateText(Question input)
    {
        question.text = input.Content;
        answers[0].text = input.Answers[0].Content;
        answers[1].text = input.Answers[1].Content;
        answers[2].text = input.Answers[2].Content;
    }
    #endregion

    #region GENERAL METHODS
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
            int j = Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
        return list;
    }
    #endregion
}
