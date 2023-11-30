using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }

    [SerializeField] private QuestionRefsSO _questionRefsSo;
    [SerializeField] private GameSettingsSO _gameSettingsSo;
    [SerializeField] private GameObject _UI;
    
    [SerializeField] private QuestionClassification _classification;

    private Question[] _questions;

    public QuestionManager()
    {
        if (_gameSettingsSo != null)
            _classification = _gameSettingsSo.Classification;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _questions = GetQuestionsByClassification(_classification);
    }

    public Question[] GetQuestionsByClassification(QuestionClassification classification)
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

    public void ShowUI(bool value)
    {
        _UI.SetActive(value);
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

    private void OnDestroy()
    {
        ResetQuestions();
    }

    private void ResetQuestions()
    {
        foreach (var question in _questions)
        {
            question.isSelected = false;
        }
    }
}
