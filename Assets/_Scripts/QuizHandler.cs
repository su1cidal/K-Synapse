using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuizHandler : MonoBehaviour
{
    public static QuizHandler Instance { get; private set; }
    
    [SerializeField] private QuizUI _quizUI;
    [SerializeField] private Timer _timer;
    [SerializeField] private Pawn[] _pawns;
    
    private List<SelectAnswers> selectAnswersList;
    private bool _isQuizEnded = false;
    private Question _currentQuestion;

    private List<Pawn> selectedFirstAnswer;
    private List<Pawn> selectedSecondAnswer;
    private List<Pawn> selectedThirdAnswer;
    private List<Pawn> selectedForthAnswer;

    public delegate void SelectAnswers(Pawn pawn);
    
    private void Awake()
    {
        Instance = this;

        _timer = GetComponent<Timer>();
        selectAnswersList = new List<SelectAnswers>();
        selectedFirstAnswer = new List<Pawn>();
        selectedSecondAnswer = new List<Pawn>();
        selectedThirdAnswer = new List<Pawn>();
        selectedForthAnswer = new List<Pawn>();
        
        selectAnswersList.Add(_quizUI.SelectFirstAnswer);
        selectAnswersList.Add(_quizUI.SelectSecondAnswer);
        selectAnswersList.Add(_quizUI.SelectThirdAnswer);
        selectAnswersList.Add(_quizUI.SelectForthAnswer);
        
        _timer.TimeEnded += OnTimeEnded;
        _quizUI.OnQuizTextReady += QuizUIOnOnQuizTextReady;
        _quizUI.OnFirstAnswer += delegate(Pawn pawn) { selectedFirstAnswer.Add(pawn); };
        _quizUI.OnSecondAnswer += delegate(Pawn pawn) { selectedSecondAnswer.Add(pawn); };
        _quizUI.OnThirdAnswer += delegate(Pawn pawn) { selectedThirdAnswer.Add(pawn); };
        _quizUI.OnForthAnswer += delegate(Pawn pawn) { selectedForthAnswer.Add(pawn); };
    }

    public IEnumerator StartQuiz()
    {
        SetPlayerPawn();
        
        _isQuizEnded = false;
        
        _currentQuestion = QuestionManager.Instance.GetRandomQuestion();
        _quizUI.SetQuizText(_currentQuestion);
        
        StartCoroutine(AnswerForBots());
        
        yield return new WaitUntil(IsQuizEnded);
    }

    private void SetPlayerPawn()
    {
        foreach (var pawn in _pawns)
        {
            if (pawn.IsPlayer())
            {
                _quizUI.SetPlayer(pawn);
            }
        }
    }

    public void EndQuiz()
    {
        // todo Show correct answer
        // todo check results. Is Answers correct, or damage players who doesnt answer at all
        _timer.StopTimer();
        _quizUI.Hide();

        _isQuizEnded = true;

        ClearPawnAnswers();
    }

    private void ClearPawnAnswers()
    {
        foreach (var pawn in _pawns)
        {
            pawn.IsAnswered = false;
        }
    }

    private IEnumerator AnswerForBots()
    {
        int waitTime = Random.Range(1, 2);
        
        foreach (var pawn in _pawns)
        {
            if (!pawn.IsPlayer())
            {
                var answerMethod = selectAnswersList.PickRandom();
                answerMethod(pawn);
            }
            yield return new WaitForSeconds(waitTime);
        }
        
        
    }
    
    private void QuizUIOnOnQuizTextReady()
    {
        _timer.StartTimer();
        _quizUI.Show();
    }
    
    private void OnTimeEnded()
    {
        //todo create a method than return all correct answers
        //get listS with players who selected correct answer -> assign them keys
        //get other lists, and remove keys to players in it
        //than highlight correct answer at UI                      Probably should use IEnumerator
        
        EndQuiz();
    }
    
    private bool IsQuizEnded()
    {
        return _isQuizEnded;
    }

    private void OnDestroy()
    {
        _timer.TimeEnded -= OnTimeEnded;
        _quizUI.OnQuizTextReady -= QuizUIOnOnQuizTextReady;
    }
}