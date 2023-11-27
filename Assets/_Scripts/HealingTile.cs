using UnityEngine;

namespace _Scripts
{
    public class HealingTile : Tile
    {
        public override void DoAction(Pawn player)
        {
            Debug.Log("HealingTile Action");
            player.AddHealth(Constants.TILE_ADD_HEALTH);
        }
    }
}