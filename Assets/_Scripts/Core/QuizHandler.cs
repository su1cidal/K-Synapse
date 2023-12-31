using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuizHandler : MonoBehaviour
{
    public static QuizHandler Instance { get; private set; }
    
    [SerializeField] private QuizUI _quizUI;
    [SerializeField] private Timer _timer;
    [SerializeField] private PawnRepository _pawnRepository;
    
    private List<SelectAnswers> selectAnswersList;
    private bool _isQuizEnded = false;
    private Question _currentQuestion;

    private List<Pawn> selectedFirstAnswer;
    private List<Pawn> selectedSecondAnswer;
    private List<Pawn> selectedThirdAnswer;
    private List<Pawn> selectedForthAnswer;
    private Answer[] sortedAnswers;
    private List<List<Pawn>> listOfListsOfAnswers;
    
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
        listOfListsOfAnswers = new List<List<Pawn>>();
        
        selectAnswersList.Add(_quizUI.SelectFirstAnswer);
        selectAnswersList.Add(_quizUI.SelectSecondAnswer);
        selectAnswersList.Add(_quizUI.SelectThirdAnswer);
        selectAnswersList.Add(_quizUI.SelectForthAnswer);
        
        listOfListsOfAnswers.Add(selectedFirstAnswer);
        listOfListsOfAnswers.Add(selectedSecondAnswer);
        listOfListsOfAnswers.Add(selectedThirdAnswer);
        listOfListsOfAnswers.Add(selectedForthAnswer);
        
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
        _quizUI.EnableButtons();
        
        _isQuizEnded = false;
        
        _currentQuestion = QuestionManager.Instance.GetRandomQuestion();
        sortedAnswers = _quizUI.SetQuizText(_currentQuestion);
        
        StartCoroutine(AnswerForBots());
        
        yield return new WaitUntil(IsQuizEnded);
    }

    private void SetPlayerPawn()
    {
        foreach (var pawn in _pawnRepository.GetPawns())
        {
            if (pawn.IsPlayer)
            {
                _quizUI.SetPlayer(pawn);
            }
        }
    }

    public IEnumerator EndQuiz()
    {
        _timer.StopTimer();
        _quizUI.DisableButtons();
        
        var indices = GetCorrectAnswersIndices();
        StartCoroutine(_quizUI.ShowCorrectAnswers(indices));
        
        yield return StartCoroutine(ProceedQuizResults(indices));
        
        _quizUI.Hide();
        ClearPawnIsAnswered();
        ClearPreviousAnswers();
        
        _isQuizEnded = true;
    }

    private IEnumerator ProceedQuizResults(List<int> indices)
    {
        var wrongPlayers = _pawnRepository.GetPawns().ToList();

        foreach (var index in indices)
        {
            var correctPlayers = listOfListsOfAnswers[index];

            foreach (var player in correctPlayers)
            {
                player.AddKeys(Constants.KEYS_TO_ADD_AFTER_CORRECT_ANSWER);
                player.AddCorrectAnswer(_currentQuestion);
                wrongPlayers.Remove(player);
            }
        }

        foreach (var player in wrongPlayers)
        {
            player.RemoveKeys(Constants.KEYS_TO_REMOVE_AFTER_CORRECT_ANSWER);
            player.AddWrongAnswer(_currentQuestion);
        }
        yield return new WaitForSeconds(3f);
    }

    private void ClearPreviousAnswers()
    {
        foreach (var list in listOfListsOfAnswers)
        {
            list.Clear();
        }
    }

    private void ClearPawnIsAnswered()
    {
        foreach (var pawn in _pawnRepository.GetPawns())
        {
            pawn.IsAnswered = false;
        }
    }

    private IEnumerator AnswerForBots()
    {
        float waitTime = Random.Range(0.5f, 1.4f);
        
        foreach (var pawn in _pawnRepository.GetPawns())
        {
            if (!pawn.IsPlayer)
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

    private List<int> GetCorrectAnswersIndices()
    {
        List<int> correctAnswersIndices = new List<int>();
        
        for (int i = 0; i < sortedAnswers.Length; i++)
        {
            if(sortedAnswers[i].isCorrect)
                correctAnswersIndices.Add(i);
        }

        return correctAnswersIndices;
    }
    
    private void OnTimeEnded()
    {
        StartCoroutine(EndQuiz());
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