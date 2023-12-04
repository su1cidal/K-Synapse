using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance { get; private set; }

    [SerializeField] private GameSettingsSO _gameSettings;
    [SerializeField] private PawnMover _pawnMover;
    [SerializeField] private List<Pawn> _pawns;
    [SerializeField] private Map _map;

    private GameState _gameState;

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
        // todo ReadGameSettings()
    }

    private void Start()
    {
        SwitchGameState(GameState.SpawnPlayers);
        // todo create players from prefabs
    }

    private void SpawnPawns()
    {
        var startTile = _map.GetStartTile();
        foreach (var pawn in _pawns)
        {
            startTile.AddPawn(pawn);
            pawn.currentMapTile = startTile;
            pawn.GetComponentInChildren<Transform>().position = pawn.currentMapTile.transform.position;
        }

        Debug.Log("All players were spawned!");
        SwitchGameState(GameState.RollADice);
    }

    private void RollADice()
    {
        if (_pawns.Count >= 1)
        {
            foreach (var pawn in _pawns)
            {
                pawn.RollADice();
            }
        }

        Debug.Log("All players rolled a dice!");

        SwitchGameState(GameState.SortMoveOrder);
    }

    private IEnumerator MovePawns()
    {
        foreach (var pawn in _pawns)
        {
            yield return StartCoroutine(_pawnMover.MoveByPath(pawn));
        }

        Debug.Log("All players were moved!");
        
        //todo checkIfGameEnded
        SwitchGameState(GameState.RollADice);
    }

    private void SwitchGameState(GameState gameState)
    {
        _gameState = gameState;

        switch (_gameState)
        {
            case GameState.SpawnPlayers:
                Debug.Log("Entered state SpawnPlayers");
                SpawnPawns();
                break;
            case GameState.RollADice:
                Debug.Log("Entered state RollADice");
                RollADice();
                break;
            case GameState.SortMoveOrder:
                Debug.Log("Entered state SortMoveOrder");
                SortMoveOrder();
                break;
            case GameState.DoMoves:
                Debug.Log("Entered state DoMoves");
                StartCoroutine(MovePawns());
                break;
            case GameState.CycleAction:
                Debug.Log("Entered state CycleAction");
                //todo Show question interaction window

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SortMoveOrder()
    {
        StringBuilder sortedPawns = new StringBuilder();

        _pawns = _pawns.OrderByDescending(o => o.rolledDice).ToList();

        foreach (var pawn in _pawns)
        {
            sortedPawns.Append($"{pawn.name}({pawn.rolledDice})|");
        }

        Debug.Log($"Sorted order: {sortedPawns}");

        SwitchGameState(GameState.DoMoves);
    }
}