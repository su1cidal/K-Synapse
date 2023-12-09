using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettingsSO;
    
    [Header("Name")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _name;
    
    [Header("Pawn")]
    [SerializeField] private GameObject _photostudio;
    [SerializeField] private MeshRenderer _pawnMeshRenderer;
    [SerializeField] private PlayerMaterialsSO _selectedMaterial;
    [SerializeField] private List<PlayerMaterialsSO> _playerMaterials;
    [SerializeField] private Button _nextMaterial;

    [Header("PlayerCount")]
    [SerializeField] private TMP_Text _playerCountText;
    [SerializeField] private Button _playerCountIncrease;
    [SerializeField] private Button _playerCountDecrease;
    
    [Header("TurnCount")]
    [SerializeField] private TMP_Text _turnCountText;
    [SerializeField] private Button _turnCountIncrease;
    [SerializeField] private Button _turnCountDecrease;
    
    [Header("CupsToWin")]
    [SerializeField] private TMP_Text _cupsToWinText;
    [SerializeField] private Button _cupsToWinIncrease;
    [SerializeField] private Button _cupsToWinDecrease;
    
    [Header("TimeToAnswer")]
    [SerializeField] private TMP_Text _timeToAnswerText;
    [SerializeField] private Button _timeToAnswerIncrease;
    [SerializeField] private Button _timeToAnswerDecrease;
    
    [Header("QuestionTheme")]
    [SerializeField] private TMP_Text _questionThemeText;
    [SerializeField] private TMP_Dropdown _questionThemeDropdown;
    [SerializeField] private Button _questionThemeChange;
    [SerializeField] private QuestionRefsSO _questionRefsSO;
    [SerializeField] private QuestionsSO _selectedTheme;
    
    [Header("Navigation")]
    [SerializeField] private Button _startGame;

    private const string GAME_SCENE_NAME = "Game";
    
    private bool _isCorrectName = false;
    
    private void Awake()
    {
        _startGame.GetComponent<Button>().onClick.AddListener(StartGame); //todo
        _nextMaterial.GetComponent<Button>().onClick.AddListener(ChangeColor);
        
        _inputField.GetComponent<TMP_InputField>().onValueChanged.AddListener(CheckName);
        _inputField.GetComponent<TMP_InputField>().onDeselect.AddListener(CheckNameOnDeselect);
        
        _playerCountDecrease.GetComponent<Button>().onClick.AddListener( delegate { DecreaseFieldValue(_playerCountText, Constants.PLAYER_COUNT_MIN); });
        _playerCountIncrease.GetComponent<Button>().onClick.AddListener( delegate { IncreaseFieldValue(_playerCountText, Constants.PLAYER_COUNT_MAX); });
        
        _turnCountDecrease.GetComponent<Button>().onClick.AddListener( delegate { DecreaseFieldValue(_turnCountText, Constants.TURN_COUNT_MIN); });
        _turnCountIncrease.GetComponent<Button>().onClick.AddListener( delegate { IncreaseFieldValue(_turnCountText, Constants.TURN_COUNT_MAX); });
        
        _cupsToWinDecrease.GetComponent<Button>().onClick.AddListener( delegate { DecreaseFieldValue(_cupsToWinText, Constants.CUPS_TO_WIN_MIN); });
        _cupsToWinIncrease.GetComponent<Button>().onClick.AddListener( delegate { IncreaseFieldValue(_cupsToWinText, Constants.CUPS_TO_WIN_MAX); });
        
        _timeToAnswerDecrease.GetComponent<Button>().onClick.AddListener( delegate { DecreaseFieldValue(_timeToAnswerText, Constants.TIMER_TO_ANSWER_MIN); });
        _timeToAnswerIncrease.GetComponent<Button>().onClick.AddListener( delegate { IncreaseFieldValue(_timeToAnswerText, Constants.TIMER_TO_ANSWER_MAX); });

        _questionThemeChange.GetComponent<Button>().onClick.AddListener(ChangeQuestionTheme);
    }

    public void StartGame()
    {
        CheckName(_inputField.text);
        
        if (_isCorrectName)
        {
            SaveToGameSettingSO();
            SceneManager.LoadScene(GAME_SCENE_NAME, LoadSceneMode.Single);
        }
    }
    
    public void Show()
    {
        LoadFromGameSettingSO();
        
        _photostudio.SetActive(true);
        this.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        _photostudio.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void LoadFromGameSettingSO()
    {
        _inputField.text = _gameSettingsSO.playerName;
        _selectedMaterial = _gameSettingsSO.playerMaterials;
        _pawnMeshRenderer.material = _selectedMaterial.PlayerColor;
        
        _playerCountText.text = _gameSettingsSO.playerCount.ToString();
        _turnCountText.text = _gameSettingsSO.turnCount.ToString();
        _cupsToWinText.text = _gameSettingsSO.cupsToWin.ToString();
        _timeToAnswerText.text = _gameSettingsSO.timeToAnswer.ToString(CultureInfo.InvariantCulture);

        _questionThemeText.text = _questionRefsSO.questionSO[0].classification.ToString();
        _selectedTheme = _questionRefsSO.questionSO[0];

        foreach (var questionSet in _questionRefsSO.questionSO)
        {
            _questionThemeDropdown.options.Add(new TMP_Dropdown.OptionData() {text=questionSet.classification.ToString()});
        }
    }
    
    private void SaveToGameSettingSO()
    {
        _gameSettingsSO.playerCount = Convert.ToInt32(_playerCountText.text);
        _gameSettingsSO.turnCount = Convert.ToInt32(_turnCountText.text);
        _gameSettingsSO.cupsToWin = Convert.ToInt32(_cupsToWinText.text);
        _gameSettingsSO.timeToAnswer = Convert.ToSingle(_timeToAnswerText.text);
        _gameSettingsSO.playerName = _name.text;
        _gameSettingsSO.playerMaterials = _selectedMaterial;
        Enum.TryParse(_questionThemeText.text, out _gameSettingsSO.classification);
    }
    
    private void IncreaseFieldValue(TMP_Text text, int maxValue)
    {
        Int32.TryParse(text.text, out var result);

        if (result < maxValue)
        {
            text.text = $"{result + 1}";
        }
    }
    
    private void DecreaseFieldValue(TMP_Text text, int minValue)
    {
        Int32.TryParse(text.text, out var result);

        if (result > minValue)
        {
            text.text = $"{result - 1}";
        }
    }

    private void ChangeColor()
    {
        int materialIndex = _playerMaterials.IndexOf(_selectedMaterial);
        _selectedMaterial = _playerMaterials[(materialIndex + 1) % _playerMaterials.Count];

        _pawnMeshRenderer.material = _selectedMaterial.PlayerColor;
    }
    
    private void ChangeQuestionTheme()
    {
        int themeIndex = Array.IndexOf(_questionRefsSO.questionSO, _selectedTheme);
        
        _selectedTheme = _questionRefsSO.questionSO[(themeIndex + 1) % _questionRefsSO.questionSO.Length];

        _questionThemeText.text = _selectedTheme.classification.ToString();
    }

    private void CheckName(string value)
    {
        if (String.IsNullOrEmpty(value))
        {
            _isCorrectName = false;
            
            _inputField.text = "";
            _name.color = Color.red;
        }
        else
        {
            _isCorrectName = true;
            _name.color = Color.white;
        }
    }

    private void CheckNameOnDeselect(string value)
    {
        if (String.IsNullOrWhiteSpace(value))
        {
            _isCorrectName = false;
            _name.text = "";
            _inputField.text = "";
            _inputField.placeholder.GetComponent<TMP_Text>().color = Color.red; 
        }
        else
        {
            _isCorrectName = true;
            _inputField.placeholder.GetComponent<TMP_Text>().color = Color.white;
        }
    }
}
