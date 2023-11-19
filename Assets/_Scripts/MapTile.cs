using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace _Scripts
{
    public class MapTile : MonoBehaviour
    {
        [SerializeField] private TileType _tileType;
        [SerializeField] public List<Pawn> pawnsOnTile;
        [SerializeField] public List<MapTile> adjacentTiles;

        public void DoAction(Pawn player)
        {
            switch (_tileType)
            {
                case TileType.Empty:
                    OnEmptyTile(player);
                    break;
                case TileType.Start:
                    OnStartTile(player);
                    break;
                case TileType.Treasure:
                    OnTreasureTile(player);
                    break;
                case TileType.Key:
                    OnKeyTile(player);
                    break;
                case TileType.Healing:
                    OnHealingTile(player);
                    break;
                case TileType.Skull:
                    OnSkullTile(player);
                    break;
                case TileType.Question:
                    OnQuestionTile(player);
                    break;
                case TileType.Respawn:
                    OnRespawnTile(player);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnRespawnTile(Pawn player)
        {
            Debug.Log("Respawn Tile!");
        }

        private void OnQuestionTile(Pawn player)
        {
            Debug.Log("Question Tile!");
        }

        private void OnSkullTile(Pawn player)
        {
            Debug.Log("Skull Tile!");
            player.DoDamage(Constants.TILE_DO_DAMAGE);
        }

        private void OnHealingTile(Pawn player)
        {
            Debug.Log("Healing Tile!");
            player.AddHealth(Constants.TILE_ADD_HEALTH);
        }

        private void OnKeyTile(Pawn player)
        {
            Debug.Log("Key Tile!");
            player.AddKeys(Constants.KEYS_TO_ADD_AT_KEYS_TILE);
        }

        private void OnTreasureTile(Pawn player)
        {
            Debug.Log("Treasure Tile!");
            if (player.keys >= Constants.KEYS_TO_OPEN_TREASURE)
            {
                //todo Show treasure interaction window
            }
        }

        private void OnStartTile(Pawn player)
        {
            Debug.Log("Start Tile!");
        }

        private void OnEmptyTile(Pawn player)
        {
            Debug.Log("Empty");
        }
        
        
        
        private void OnPawnFinalMove(Pawn pawn)
        {
            if (pawnsOnTile.Count < 2) return;
            
            var rand = new Random();

            var move = rand.Next(0, 3);
            
            switch (move)
            {
                case 0:
                    pawn.visual.transform.position += new Vector3(+Constants.PAWN_OFFSET, 0f, +Constants.PAWN_OFFSET);
                    break;
                case 1:
                    pawn.visual.transform.position += new Vector3(+Constants.PAWN_OFFSET, 0f, -Constants.PAWN_OFFSET);
                    break;
                case 2:
                    pawn.visual.transform.position += new Vector3(-Constants.PAWN_OFFSET, 0f, +Constants.PAWN_OFFSET);
                    break;
                case 3:
                    pawn.visual.transform.position += new Vector3(-Constants.PAWN_OFFSET, 0f, -Constants.PAWN_OFFSET);
                    break;
            }
            
            pawn.SetLastPositionEdit(move);
            //todo add compare to other Pawns -> If it matches move one of them
        }

        private void FixPawnTransforms(Pawn pawn)
        {
            int? move = pawn.GetLastPositionEdit();
            pawn.SetLastPositionEdit(-1);
            switch (move)
            {
                case 0:
                    pawn.visual.transform.position -= new Vector3(+Constants.PAWN_OFFSET, 0f, +Constants.PAWN_OFFSET);
                    break;
                case 1:
                    pawn.visual.transform.position -= new Vector3(+Constants.PAWN_OFFSET, 0f, -Constants.PAWN_OFFSET);
                    break;
                case 2:
                    pawn.visual.transform.position -= new Vector3(-Constants.PAWN_OFFSET, 0f, +Constants.PAWN_OFFSET);
                    break;
                case 3:
                    pawn.visual.transform.position -= new Vector3(-Constants.PAWN_OFFSET, 0f, -Constants.PAWN_OFFSET);
                    break;
            }
        }

        public void AddPawn(Pawn pawn)
        {
            if (!pawnsOnTile.Contains(pawn))
            {
                pawnsOnTile.Add(pawn);
                
                if (pawnsOnTile.Count >= 2)
                {
                    OnPawnFinalMove(pawn);
                }
            }
        }
        
        public void RemovePawn(Pawn pawn)
        {
            if (pawnsOnTile.Contains(pawn))
            {
                pawnsOnTile.Remove(pawn);
                FixPawnTransforms(pawn);
            }
        }

        public TileType GetTileType()
        {
            return _tileType;
        }
    }
}