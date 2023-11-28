using System.Collections;
using System.Collections.Generic;
using _Scripts.Tiles;
using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class PawnMover : MonoBehaviour
    {
        public List<Vector3> GetPath(Pawn pawn)
        {
            List<Vector3> path = new List<Vector3>();
            List<Tile> nextTiles;
            
            Tile firstTile = pawn.currentMapTile;
            Tile tempCurrent = pawn.currentMapTile;
            int dice = pawn.rolledDice;
            
            pawn.currentMapTile.RemovePawn(pawn);
            
            for (int i = 0; i < pawn.rolledDice; ++i)
            {
                nextTiles = tempCurrent.adjacentTiles;
                if (nextTiles.Count == 1)
                {
                    tempCurrent = nextTiles[0];
                    path.Add(tempCurrent.transform.position);
                    
                    dice -= 1;
                }
                else
                {
                    Debug.LogError("More or less than one tile!");
                    // todo logic when there are more or less than 1 tile
                    break;
                }
            }
            
            pawn.currentMapTile = tempCurrent;
            pawn.currentMapTile.AddPawn(pawn);
            
            return path;
        }
        
        public IEnumerator MoveByPath(Pawn pawn)
        {
            List<Vector3> path = GetPath(pawn);
            float delay = 0.6f;
            
            for (var index = 0; index < path.Count; index++)
            {
                Vector3 point = path[index];
                pawn.transform.DOMove(point, 0.5f);
                pawn.rolledDice -= 1;
                
                yield return new WaitForSeconds(delay);
            }
            
            if (pawn.rolledDice == 0)
                yield return new WaitUntil(() => pawn.currentMapTile.DoAction(pawn)); 

            GameLoop.Instance.isFinished = true;
        }
    }
}