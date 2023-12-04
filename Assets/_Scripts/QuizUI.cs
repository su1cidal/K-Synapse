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
    [SerializeField] private GameObject _answersVisual1;
    [SerializeField] private GameObject _answersVisual2;
    [SerializeField] private GameObject _answersVisual3;
    [SerializeField] private GameObject _answersVisual4;
    [SerializeField] private GameObject _answerVisual;
    
    public event Action OnQuizTextReady;
    public event Action<Pawn> OnFirstAnswer;
    public event Action<Pawn> OnSecondAnswer;
    public event Action<Pawn> OnThirdAnswer;
    public event Action<Pawn> OnForthAnswer;

    private Pawn player;
    
    private void Start()
    {
        Hide();
    }

    public void SetPlayer(Pawn pawn)
    {
        player = pawn;
    }
    
    public void SelectFirstAnswer(Pawn pawn)
    {
        AddAnswer(pawn, _answersVisual1);
        OnFirstAnswer?.Invoke(pawn);
    }
    
    public void SelectSecondAnswer(Pawn pawn)
    {
        AddAnswer(pawn, _answersVisual2);
        OnSecondAnswer?.Invoke(pawn);
    }
    
    public void SelectThirdAnswer(Pawn pawn)
    {
        AddAnswer(pawn, _answersVisual3);
        OnThirdAnswer?.Invoke(pawn);
    }
    
    public void SelectForthAnswer(Pawn pawn)
    {
        AddAnswer(pawn, _answersVisual4);
        OnForthAnswer?.Invoke(pawn);
    }

    public void Show()
    {
        _UI.SetActive(true);
    }
    
    public void Hide()
    {
        _UI.SetActive(false);
        
        ClearAsnwersVisual(_answersVisual1);
        ClearAsnwersVisual(_answersVisual2);
        ClearAsnwersVisual(_answersVisual3);
        ClearAsnwersVisual(_answersVisual4);
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

    private void AddAnswer(Pawn pawn, GameObject answersVisual)
    {
        if(pawn.IsAnswered) return;
        
        GameObject newAnswer = Instantiate(_answerVisual, answersVisual.transform);
        
        var image = newAnswer.GetComponent<Image>();
        var material = pawn.GetAnswerMaterial();
        image.material = material;

        pawn.IsAnswered = true;
    }
    
    private void ClearAsnwersVisual(GameObject answers)
    {
        foreach (Transform child in answers.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
    
    private void OnEnable()
    {
        _answer1Button.GetComponent<Button>().onClick.AddListener(delegate { SelectFirstAnswer(player); });
        _answer2Button.GetComponent<Button>().onClick.AddListener(delegate { SelectSecondAnswer(player); });
        _answer3Button.GetComponent<Button>().onClick.AddListener(delegate { SelectThirdAnswer(player); });
        _answer4Button.GetComponent<Button>().onClick.AddListener(delegate { SelectForthAnswer(player); });
    }

    private void OnDestroy()
    {
        _answer1Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectFirstAnswer(player); });
        _answer2Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectSecondAnswer(player); });
        _answer3Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectThirdAnswer(player); });
        _answer4Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectForthAnswer(player); });
    }
}