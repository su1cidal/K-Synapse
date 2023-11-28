using UnityEngine;

namespace _Scripts.Tiles
{
    public class HealingTile : Tile
    {
        public override bool DoAction(Pawn player)
        {
            Debug.Log("HealingTile Action");
            player.AddHealth(Constants.TILE_ADD_HEALTH);

            return true;
        }
    }
}