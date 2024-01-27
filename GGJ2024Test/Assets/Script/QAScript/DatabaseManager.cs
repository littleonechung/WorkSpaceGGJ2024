using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private QuestionDatabase _questionDatabase;

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
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
    #endregion
}
