using UnityEngine;

namespace _Scripts.Tiles
{
    public class StartTile : Tile
    {
        public override bool DoAction(Pawn player)
        {
            Debug.Log("StartTile Action!");
            
            return true;
        }
    }
}