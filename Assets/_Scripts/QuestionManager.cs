using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }

    [SerializeField] private QuestionRefsSO _questionRefsSo;
    [SerializeField] private GameSettingsSO _gameSettingsSo;
    
    [SerializeField] private QuestionClassification _classification;

    private Question[] _questions;

    private void Awake()
    {
        Instance = this;
        
        _classification = _gameSettingsSo.classification;
    }

    private void Start()
    {
        _questions = GetQuestionsByClassification(_classification);
    }

    private Question[] GetQuestionsByClassification(QuestionClassification classification)
    {
        foreach (var questionSet in _questionRefsSo.questionSO)
        {
            if (classification == questionSet.classification)
            {
                return questionSet.questions;
            }
        }
        
        return null;
    }

    public Question GetRandomQuestion()
    {
        List<Question> notSelected = new List<Question>();
        Random rand = new Random();
        Question selected;
        
        foreach (var question in _questions)
        {
            if (question.isSelected == false)
            {
                notSelected.Add(question);
            }
        }

        if (notSelected.Count <= 0)
        {
            ResetQuestions();
            notSelected.AddRange(_questions);
        }
        
        int toSkip = rand.Next(0, notSelected.Count());
        
        selected = notSelected.Skip(toSkip).Take(1).First(o => o.isSelected == false);
        selected.isSelected = true;
        
        Debug.Log(selected.question);
        return selected;
    }
    
    private void ResetQuestions()
    {
        foreach (var question in _questions)
        {
            question.isSelected = false;
        }
    }

    private void OnDestroy()
    {
        ResetQuestions();
    }
}
