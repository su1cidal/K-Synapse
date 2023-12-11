using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance { get; private set; }

    [Header("Game Related")]
    [SerializeField] private GameSettingsSO _gameSettingsSO;
    [SerializeField] private PawnMover _pawnMover;
    [SerializeField] private PawnRepository _pawnRepository;
    [SerializeField] private CupsToWinUI _cupsToWinUI;
    [SerializeField] private TurnCountUI _turnCountUI;
    [SerializeField] private TurnOrderUI _turnOrderUI;
    [SerializeField] private ScoreboardUI _scoreboardUI;
    [SerializeField] private GameObject _playerTurn;
    [SerializeField] private GameObject _inputGuide;
    [SerializeField] private TMP_Text _playerTurnText;
    [SerializeField] private Map _map;
    [SerializeField] private GameState _gameState;
    [SerializeField] private int _timeScale = 1;

    [Header("EndGame")]
    [SerializeField] private GameObject _endGameScene;
    [SerializeField] private GameObject _topLeftUI;
    [SerializeField] private GameObject _topRightUI;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _firstPlace;
    [SerializeField] private GameObject _secondPlace;
    [SerializeField] private GameObject _thirdPlace;
    [SerializeField] private GameObject _lastPlace;
    
    private List<Pawn> _pawns;
    private int _turnCount = 0;
    private Camera _mainCamera;
    
    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        _menuButton.GetComponent<Button>().onClick.AddListener(GoToMainMenu);
        
        LoadFromGameSettings();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        
        SwitchGameState(GameState.SpawnPlayers);
    }

    private void Update()
    {
        Time.timeScale = _timeScale;
    }

    private void LoadFromGameSettings()
    {
        //UI setup
        _cupsToWinUI.SetCupsCount(_gameSettingsSO.cupsToWin);
        _turnCountUI.SetTurnCount(_turnCount, _gameSettingsSO.turnCount);
        
        //Pawn creation
        _pawnRepository.InitializePawns(_gameSettingsSO);
        _pawns = _pawnRepository.GetPawns();
    }
    
    
    private void SpawnPawns()
    {
        var startTile = _map.GetStartTile();
        
        foreach (var pawn in _pawns)
        {
            startTile.AddPawn(pawn);
            pawn.currentMapTile = startTile;
            pawn.transform.position = pawn.currentMapTile.transform.position;
        }

        Debug.Log("All players were spawned!");
        
        SwitchGameState(GameState.RollADice);
    }

    private IEnumerator RollADices()
    {
        foreach (var pawn in _pawns)
        {
            yield return StartCoroutine(pawn.RollADice());
        }

        Debug.Log("All players rolled a dice!");

        SwitchGameState(GameState.SortMoveOrder);
    }

    private IEnumerator MovePawns()
    {
        foreach (var pawn in _pawns)
        {
            StartCoroutine(ShowPlayerTurnText(pawn));
            yield return StartCoroutine(_pawnMover.MoveByPath(pawn));
        }

        Debug.Log("All players were moved!");
        
        _turnCount++;
        _turnCountUI.SetTurnCount(_turnCount, _gameSettingsSO.turnCount);
        
        SwitchGameState(GameState.IsEnd);
    }

    private IEnumerator ShowPlayerTurnText(Pawn pawn)
    {
        _playerTurnText.color = pawn.materials.PlayerColor.color;
        _playerTurnText.text = pawn.playerName + " TURN!";
        _playerTurn.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _playerTurn.SetActive(false);
    }

    private void SwitchGameState(GameState gameState)
    {
        _gameState = gameState;

        switch (_gameState)
        {
            case GameState.SpawnPlayers:
                Debug.Log("Entered state SpawnPlayers\n");
                SpawnPawns();
                break;
            case GameState.RollADice:
                Debug.Log("Entered state RollADice\n");
                StartCoroutine(RollADices());
                break;
            case GameState.SortMoveOrder:
                Debug.Log("Entered state SortMoveOrder\n");
                SortMoveOrder();
                break;
            case GameState.DoMoves:
                Debug.Log("Entered state DoMoves\n");
                StartCoroutine(MovePawns());
                break;
            case GameState.IsEnd:
                Debug.Log("Entered state IsEnd\n");
                if (!CheckIsEnd())
                    SwitchGameState(GameState.RollADice);
                else
                    StartCoroutine(EndGame());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GoToMainMenu()
    {
        _mainCamera.gameObject.SetActive(true);
        _topRightUI.SetActive(true);
        _topLeftUI.SetActive(true);
        
        _endGameScene.SetActive(false);
        _scoreboardUI.Hide();
        _menuButton.gameObject.SetActive(false);

        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME, LoadSceneMode.Single);
    }
    
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5f);
        _topLeftUI.SetActive(false);
        _topRightUI.SetActive(false);
        _inputGuide.SetActive(false);
        _mainCamera.gameObject.SetActive(false);
        
        _endGameScene.SetActive(true);
        _scoreboardUI.Show();
        _menuButton.gameObject.SetActive(true);

        foreach (var pawn in _pawnRepository.GetPawns())
        {
            if (pawn.place == _gameSettingsSO.playerCount)
            {
                pawn.transform.parent = _lastPlace.transform;
                ZeroPawnPosition(pawn);
            }
            
            if (pawn.place == 1)
            {
                pawn.transform.parent = _firstPlace.transform;
                ZeroPawnPosition(pawn);
            }

            if (pawn.place == 2)
            {
                pawn.transform.parent = _secondPlace.transform;
                ZeroPawnPosition(pawn);
            }

            if (pawn.place == 3)
            {
                pawn.transform.parent = _thirdPlace.transform;
                ZeroPawnPosition(pawn);
            }
        }
    }

    private void ZeroPawnPosition(Pawn pawn)
    {
        Vector3 newVector = new Vector3(0,0,0);
        pawn.visual.transform.localPosition = newVector;
        pawn.transform.localPosition = newVector;
    }
    
    private bool CheckIsEnd()
    {
        if (_turnCount == _gameSettingsSO.turnCount)
        {
            return true;
        }

        foreach (var pawn in _pawnRepository.GetPawns())
        {
            if (pawn.cups == _gameSettingsSO.cupsToWin)
            {
                return true;
            }
        }

        return false;
    }
    
    private void SortMoveOrder()
    {
        _pawns = _pawns.OrderByDescending(o => o.rolledDice).ToList();
        
        _turnOrderUI.SetTurnOrder(_pawns);

        SwitchGameState(GameState.DoMoves);
    }

    private void OnDisable()
    {
        _menuButton.onClick.RemoveAllListeners();
    }
}