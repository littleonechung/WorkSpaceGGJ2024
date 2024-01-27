using System.Collections.Generic;
using UnityEngine;
using QuestionDict = System.Collections.Generic.List<QuestionDictItem>;

[System.Serializable]
public struct Answer
{
    [SerializeField] public string Content;
    [SerializeField] public string Respondse;
    [SerializeField] public int Score;
    [SerializeField] public bool IsCorrect;
}
[System.Serializable]
public struct Question
{
    [SerializeField] public string Content;
    [SerializeField] public List<Answer> Answers;
}
[System.Serializable]
public struct QuestionDictItem
{
    [SerializeField] public GodName Key;
    [SerializeField] public List<Question> Questions;
}

[CreateAssetMenu]
public class QuestionDatabase : ScriptableObject
{
    [Header("QA List")]
    [SerializeField] public QuestionDict script;
}
