using UnityEngine;

namespace _Scripts
{
    public class QuestionTile : Tile
    {
        public override void DoAction(Pawn player)
        {
            Debug.Log("QuestionTile Action");
            // todo call QuestionManager to display question
        }
    }
}