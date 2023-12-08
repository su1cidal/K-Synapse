using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    [Header("Name")]
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
    
    [Header("GameMode")]
    [SerializeField] private TMP_Text _gameModeText;
    [SerializeField] private Button _gameModeIncrease;
    [SerializeField] private Button _gameModeDecrease;
    
    [Header("QuestionTheme")]
    [SerializeField] private TMP_Text _questionThemeText;
    [SerializeField] private Button _questionThemeIncrease;
    [SerializeField] private Button _questionThemeDecrease;
    
    [Header("Navigation")]
    [SerializeField] private Button _back;
    [SerializeField] private Button _StartGame;
    
    private void Awake()
    {
        _nextMaterial.GetComponent<Button>().onClick.AddListener(ChangeColor);
    }

    private void Show()
    {
        _photostudio.SetActive(true);
    }

    private void ChangeColor()
    {
        int materialIndex = _playerMaterials.IndexOf(_selectedMaterial);
        _selectedMaterial = _playerMaterials[(materialIndex + 1) % _playerMaterials.Count];

        _pawnMeshRenderer.material = _selectedMaterial.PlayerColor;
    }
}
