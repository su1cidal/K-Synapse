using System.Collections;
using UnityEngine;

public class QuizHandler : MonoBehaviour
{
    public static QuizHandler Instance { get; private set; }

    [SerializeField] private QuizUI _quizUI;
    [SerializeField] private Timer _timer;
    
    private bool _isQuizEnded = false;
    
    private void Awake()
    {
        Instance = this;

        _timer = GetComponent<Timer>();
        
        _timer.TimeEnded += OnTimeEnded;
        _quizUI.OnQuizTextReady += QuizUIOnOnQuizTextReady;
    }

    public IEnumerator StartQuiz()
    {
        _isQuizEnded = false;
        
        Question question = QuestionManager.Instance.GetRandomQuestion();
        _quizUI.SetQuizText(question);
        
        yield return new WaitUntil(IsQuizEnded);
        //todo set new question/answers. Handle adding player's answers squares/
    }

    public void EndQuiz()
    {
        // todo Show correct answer
        // todo check results. Is Answers correct, or damage players who doesnt answer at all
        _timer.StopTimer();
        _quizUI.Hide();

        _isQuizEnded = true;
    }
    
    private void QuizUIOnOnQuizTextReady()
    {
        _timer.StartTimer();
        _quizUI.Show();
    }
    
    private void OnTimeEnded()
    {
        EndQuiz();
    }
    
    private bool IsQuizEnded()
    {
        return _isQuizEnded;
    }
}