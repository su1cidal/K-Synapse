using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Task = System.Threading.Tasks.Task;

namespace _Scripts
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Map _map;
        [SerializeField] private GameSettingsSO _gameSettings;
        
        [SerializeField] private List<Pawn> _players;
        private GameState _gameState;
        
        private void Start()
        {
            SwitchGameState(GameState.SpawnPlayers);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SwitchGameState(GameState.RollADice);
            }
        }

        private void SwitchGameState(GameState gameState)
        {
            _gameState = gameState;
            
            switch (_gameState)
            {
                case GameState.SpawnPlayers:
                    Debug.Log("Entered state SpawnPlayers");
                    SpawnPlayer();
                    break;
                case GameState.RollADice:
                    Debug.Log("Entered state RollADice");
                    PlayersRollADice();
                    break;
                case GameState.DoMoves:
                    Debug.Log("Entered state DoMoves");
                    MovePlayers();
                    break;
                case GameState.EndAction:
                    Debug.Log("End Action: Show Question Window");
                    //todo Show question interaction window
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void SpawnPlayer()
        {
            var startTile = _map.GetStartTile();
            foreach (var player in _players)
            {
                startTile.AddPawn(player);
                player.currentMapTile = startTile;
                player.GetComponentInChildren<Transform>().position = player.currentMapTile.transform.position;
            }

            Debug.Log("All players were spawned!");
            SwitchGameState(GameState.RollADice);
        }
        
        private void PlayersRollADice()
        {
            if (_players.Count >= 1)
            {
                foreach (var player in _players)
                { 
                    player.RollADice();
                }
            }
            Debug.Log("All players rolled a dice!");
            SwitchGameState(GameState.DoMoves);
        }

        private void MovePlayers()
        {
            foreach (var player in _players)
            {
                if (player != null)
                {
                    StartCoroutine(player.MoveToNextTile());
                }
            }
                            
            Debug.Log("All players were moved!");
            SwitchGameState(GameState.EndAction);
        }
    }
}