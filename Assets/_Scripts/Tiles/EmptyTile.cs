using UnityEngine;

namespace _Scripts.Tiles
{
    public class EmptyTile : Tile
    {
        public override bool DoAction(Pawn player)
        {
            Debug.Log("EmptyTile Action");
            
            return true;
        }
    }
}