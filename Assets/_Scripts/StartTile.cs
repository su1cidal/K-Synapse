using UnityEngine;

namespace _Scripts
{
    public class StartTile : Tile
    {
        public override void DoAction(Pawn player)
        {
            Debug.Log("StartTile Action!");
        }
    }
}