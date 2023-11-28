using UnityEngine;

namespace _Scripts.Tiles
{
    public class RespawnTile : Tile
    {
        public override bool DoAction(Pawn player)
        {
            Debug.Log("RespawnTile Action");
            // todo create cool respawn effect

            return true;
        }
    }
}