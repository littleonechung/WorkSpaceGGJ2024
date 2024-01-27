using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private QuestionDatabase _questionDatabase;

    public List<Question> GetQuestionListViaGodName(GodName godName)
    {
        foreach (QuestionDictItem questionDictItem in _questionDatabase.script)
            if (questionDictItem.Key == godName)
                return questionDictItem.Questions;
        return null;
    }

}
