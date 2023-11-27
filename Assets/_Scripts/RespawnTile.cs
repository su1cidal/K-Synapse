using UnityEngine;

namespace _Scripts
{
    public class RespawnTile : Tile
    {
        public override void DoAction(Pawn player)
        {
            Debug.Log("RespawnTile Action");
            // todo create cool respawn effect
        }
    }
}