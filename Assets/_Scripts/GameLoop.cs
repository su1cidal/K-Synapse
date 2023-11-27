using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using System.Threading;

namespace _Scripts
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private List<Pawn> _players;
        [SerializeField] private Map _map;
        
        private GameState _gameState;
        private int _playerCount;

        private void Awake()
        {
            // todo ReadGameSettings()
        }

        private void Start()
        {
            SwitchGameState(GameState.SpawnPlayers);
            
            // todo create players from prefabs
            
            foreach (var player in _players)
            {
                player.OnFinalStep += PlayerOnOnFinalStep;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SwitchGameState(GameState.RollADice);
            }
        }

        private void PlayerOnOnFinalStep(object sender, EventArgs e)
        {
            _playerCount--;
        }
        
        private void SwitchGameState(GameState gameState)
        {
            _playerCount = _players.Count;
            
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

            // while (!IsPlayerCountZeroed()) // https://discussions.unity.com/t/have-a-function-to-wait-until-true/49616
            // {
            //     //await Task.Delay(25);
            // }
            SwitchGameState(GameState.DoMoves);
        }

        private bool IsPlayerCountZeroed()
        {
            if (_playerCount == 0)
                return true;
            else
            {
                return false;
            }
        }

        private void MovePlayers()
        {
            foreach (var player in _players)
            {
                StartCoroutine(player.MoveByPath());
                
                Debug.Log("All players were moved!");
            }

            SwitchGameState(GameState.EndAction);
        }

        private void OnDestroy()
        {
            foreach (var player in _players)
            {
                player.OnFinalStep -= PlayerOnOnFinalStep;
            }
        }
    }
}