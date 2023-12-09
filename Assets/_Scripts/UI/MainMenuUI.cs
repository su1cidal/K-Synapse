using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _unlocks;
    [SerializeField] private Button _exit;

    [SerializeField] private TMP_Text _currentWindow;
    [SerializeField] private GameObject _mainMenu;
    
    [SerializeField] private GameSettingsUI _gameSettingsUI;
    [SerializeField] private Button _back;
    
    private void Awake()
    {
        _play.GetComponent<Button>().onClick.AddListener(ShowGameSettings);
        _settings.GetComponent<Button>().onClick.AddListener(ShowSettings);
        _unlocks.GetComponent<Button>().onClick.AddListener(ShowUnlocks);
        _exit.GetComponent<Button>().onClick.AddListener(ShowExitWindow);

        _back.GetComponent<Button>().onClick.AddListener(() => {
            HideGameSettings();
            HideOptions();
            HideUnlocks();
        });

        Application.targetFrameRate = 100;
        // todo remove targetframerate?
    }

    private void Start()
    {
        Show();
    }

    private void ShowGameSettings()
    {
        _currentWindow.text = "Game Settings";
        Hide();
        
        _gameSettingsUI.Show();
    }

    private void HideGameSettings()
    {
        Show();
        _gameSettingsUI.Hide();
        // hide GameSettings Window
    }

    private void ShowSettings()
    {
        _currentWindow.text = "Settings";
        Hide();
        // ??
    }
    
    private void HideOptions()
    {
        Show();
        // ??
    }
    
    private void ShowUnlocks()
    {
        _currentWindow.text = "Unlocks";
        Hide();
        // ??
    }

    private void HideUnlocks()
    {
        Show();
        // ??
    }

    private void ShowExitWindow()
    {
        _currentWindow.text = "Exit";
        Hide();
        // Are you sure you want quit game?
        // yes -> Application.Quit();
        Application.Quit();
    }

    private void HideExitWindow()
    {
        Show();
    }


    private void Show()
    {
        _currentWindow.text = "Main Menu";
        
        _mainMenu.SetActive(true);
        
        _back.gameObject.SetActive(false);
    }

    private void Hide()
    {
        _mainMenu.SetActive(false);
        
        _back.gameObject.SetActive(true);
    }
}
