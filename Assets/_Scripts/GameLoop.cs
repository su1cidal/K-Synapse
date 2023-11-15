using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Map _map;
        [SerializeField] private ScriptableObject _gameSettings;
        
        [SerializeField] private List<Pawn> _players;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnPlayer();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayersRollADice();
            }
            
            if (Input.GetKeyDown(KeyCode.M))
            {
                MovePlayers();
            }
        }
        
        private void MovePlayers()
        {          
            var mapTiles = _map.GetMap();

            foreach (Pawn player in _players)
            {
                for (int i = 0; i < player.rolledDice; i++)
                {
                    Debug.Log($"{player.gameObject.name} {i}" );
                    var nextTile = player.currentMapTile.adjacentTiles;

                    if (nextTile != null)
                    { 
                        player.isFinalStep = false;
                        player.currentMapTile.RemovePawn(player);
                        nextTile[0].AddPawn(player);
                        player.currentMapTile = nextTile[0];
                        player.GetComponentInChildren<Transform>().DOMove(player.currentMapTile.transform.position, 1f);//todo or *speed*Time.deltatime
                        StartCoroutine(Wait());
                        
                        // if (nextTile.Count == 1)
                        // {
                        //     
                        // }
                        // else
                        // {
                        //     Debug.Log("Two ways!");
                        //     if (Input.GetKeyDown(KeyCode.Alpha1))
                        //     {
                        //         player.currentMapTile.RemovePawn(player);
                        //         nextTile[0].AddPawn(player);
                        //     }
                        //     if (Input.GetKeyDown(KeyCode.Alpha2))
                        //     {
                        //         player.currentMapTile.RemovePawn(player);
                        //         nextTile[1].AddPawn(player);
                        //     }
                        // }
                    }

                }

                player.isFinalStep = true;
                Debug.Log(player.currentMapTile.GetTileType()); 
            }
        }
        private void SpawnPlayer()
        {
            var startTile = _map.GetStartTile();

            foreach (var player in _players)
            {
                startTile.AddPawn(player);
                player.currentMapTile = startTile;
            }
        }

        private void PlayersRollADice()
        {
            if (_players.Count >= 1)
            {
                foreach (var player in _players)
                {
                    if (player != null)
                        player.RollADice();
                }
            }
        }
        
        
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1);
        }
    }
}
