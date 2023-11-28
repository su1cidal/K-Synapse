using UnityEngine;

namespace _Scripts.Tiles
{
    public class QuestionTile : Tile
    {
        public override bool DoAction(Pawn player)
        {
            Debug.Log("QuestionTile Action");
            // todo call QuestionManager to display question
            
            return true;
        }
    }
}