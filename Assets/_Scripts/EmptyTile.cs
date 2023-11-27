using UnityEngine;

namespace _Scripts
{
    public class EmptyTile : Tile
    {
        public override void DoAction(Pawn player)
        {
            Debug.Log("EmptyTile Action");
        }
    }
}