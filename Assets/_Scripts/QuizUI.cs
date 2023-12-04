using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    
    [SerializeField] private TMP_Text _quizQuestion;
    [SerializeField] private TMP_Text _answer1;
    [SerializeField] private TMP_Text _answer2;
    [SerializeField] private TMP_Text _answer3;
    [SerializeField] private TMP_Text _answer4;
    [SerializeField] private Button _answer1Button;
    [SerializeField] private Button _answer2Button;
    [SerializeField] private Button _answer3Button;
    [SerializeField] private Button _answer4Button;
    
    public event Action OnQuizTextReady;

    private void Start()
    {
        Hide();
    }

    public void SelectFirstAnswer()// todo IPlayer player
    {
        Debug.Log("First answer is selected!");
    }
    
    public void SelectSecondAnswer()// todo IPlayer player
    {
        Debug.Log("Second answer is selected!");
    }
    
    public void SelectThirdAnswer()// todo IPlayer player
    {
        Debug.Log("Third answer is selected!");
    }
    
    public void SelectForthAnswer()// todo IPlayer player
    {
        Debug.Log("Forth answer is selected!");
    }

    public void Show()
    {
        _UI.SetActive(true);
    }
    
    public void Hide()
    {
        _UI.SetActive(false);
    }

    public void SetQuizText(Question question)
    {
        //todo clear previous player answers
        _quizQuestion.text = question.question;

        var answers = question.answers;
        var rng = new Random();
        rng.Shuffle(answers);
        
        _answer1.text = answers[0].answer;
        _answer2.text = answers[1].answer;
        _answer3.text = answers[2].answer;
        _answer4.text = answers[3].answer;
        
        OnQuizTextReady?.Invoke();
    }
    
    private void OnEnable()
    {
        _answer1Button.GetComponent<Button>().onClick.AddListener(SelectFirstAnswer);
        _answer2Button.GetComponent<Button>().onClick.AddListener(SelectSecondAnswer);
        _answer3Button.GetComponent<Button>().onClick.AddListener(SelectThirdAnswer);
        _answer4Button.GetComponent<Button>().onClick.AddListener(SelectForthAnswer);
        
    }

    private void OnDestroy()
    {
        _answer1Button.GetComponent<Button>().onClick.RemoveListener(SelectFirstAnswer);
        _answer2Button.GetComponent<Button>().onClick.RemoveListener(SelectSecondAnswer);
        _answer3Button.GetComponent<Button>().onClick.RemoveListener(SelectThirdAnswer);
        _answer4Button.GetComponent<Button>().onClick.RemoveListener(SelectForthAnswer);
    }
}