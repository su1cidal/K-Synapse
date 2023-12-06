using System;
using System.Collections;
using System.Collections.Generic;
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
    private List<Button> allAnswerButtons;

    private void Awake()
    {
        allAnswerButtons = new List<Button>();
        
        allAnswerButtons.Add(_answer1Button);
        allAnswerButtons.Add(_answer2Button);
        allAnswerButtons.Add(_answer3Button);
        allAnswerButtons.Add(_answer4Button);
        
        _answer1Button.GetComponent<Button>().onClick.AddListener(delegate { SelectFirstAnswer(player); });
        _answer2Button.GetComponent<Button>().onClick.AddListener(delegate { SelectSecondAnswer(player); });
        _answer3Button.GetComponent<Button>().onClick.AddListener(delegate { SelectThirdAnswer(player); });
        _answer4Button.GetComponent<Button>().onClick.AddListener(delegate { SelectForthAnswer(player); });
    }

    private void Start()
    {
        Hide();
    }
    
    
    public void EnableButtons()
    {
        foreach (var button in allAnswerButtons)
        {
            button.enabled = true;
        }
    }
    
    public void DisableButtons()
    {
        foreach (var button in allAnswerButtons)
        {
            button.enabled = false;
        }
    }
    
    public void SetPlayer(Pawn pawn)
    {
        player = pawn;
    }
    
    public void SelectFirstAnswer(Pawn pawn)
    {
        if(pawn.IsAnswered) return;
        AddAnswerVisual(pawn, _answersVisual1);
        OnFirstAnswer?.Invoke(pawn);
    }
    
    public void SelectSecondAnswer(Pawn pawn)
    {
        if(pawn.IsAnswered) return;
        AddAnswerVisual(pawn, _answersVisual2);
        OnSecondAnswer?.Invoke(pawn);
    }
    
    public void SelectThirdAnswer(Pawn pawn)
    {
        if(pawn.IsAnswered) return;
        AddAnswerVisual(pawn, _answersVisual3);
        OnThirdAnswer?.Invoke(pawn);
    }
    
    public void SelectForthAnswer(Pawn pawn)
    {
        if(pawn.IsAnswered) return;
        AddAnswerVisual(pawn, _answersVisual4);
        OnForthAnswer?.Invoke(pawn);
    }

    public void Show()
    {
        _UI.SetActive(true);
    }
    
    public void Hide()
    {
        _UI.SetActive(false);

        Clear();
    }

    private void Clear()
    {
        ClearAsnwerVisual(_answersVisual1);
        ClearAsnwerVisual(_answersVisual2);
        ClearAsnwerVisual(_answersVisual3);
        ClearAsnwerVisual(_answersVisual4);

        foreach (var button in allAnswerButtons)
        {
            button.interactable = true;
        }  
    }
    
    public Answer[] SetQuizText(Question question)
    {
        _quizQuestion.text = question.question;

        var answers = question.answers;
        var rng = new Random();
        rng.Shuffle(answers);
        
        _answer1.text = answers[0].answer;
        _answer2.text = answers[1].answer;
        _answer3.text = answers[2].answer;
        _answer4.text = answers[3].answer;
        
        OnQuizTextReady?.Invoke();

        return answers;
    }

    public IEnumerator ShowCorrectAnswers(List<int> indices)
    {
        foreach (var sortedAnswers in indices)
        {
            allAnswerButtons[sortedAnswers].interactable = false;
        }
        
        yield return new WaitForSeconds(3f);
    }
    
    private void AddAnswerVisual(Pawn pawn, GameObject answersVisual)
    {
        GameObject newAnswer = Instantiate(_answerVisual, answersVisual.transform);
        
        var image = newAnswer.GetComponent<Image>();
        var material = pawn.GetMaterialUI();
        image.material = material;

        pawn.IsAnswered = true;
    }
    
    private void ClearAsnwerVisual(GameObject answers)
    {
        foreach (Transform child in answers.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
    
    private void OnDestroy()
    {
        _answer1Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectFirstAnswer(player); });
        _answer2Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectSecondAnswer(player); });
        _answer3Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectThirdAnswer(player); });
        _answer4Button.GetComponent<Button>().onClick.RemoveListener(delegate { SelectForthAnswer(player); });
    }
}