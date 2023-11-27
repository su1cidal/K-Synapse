using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace _Scripts
{
    public abstract class Tile : MonoBehaviour
    {
        [SerializeField] public List<Pawn> pawnsOnTile;
        [SerializeField] public List<Tile> adjacentTiles;
        
        public abstract void DoAction(Pawn player);

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

        public void OnPawnFinalMove(Pawn pawn)
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
        
        public void FixPawnTransforms(Pawn pawn)
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
    }
}