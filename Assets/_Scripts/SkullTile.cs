using UnityEngine;

namespace _Scripts
{
    public class SkullTile : Tile
    {
        public override void DoAction(Pawn player)
        {
            Debug.Log("SkullTile Action");
            player.DoDamage(Constants.TILE_DO_DAMAGE);
        }
    }
}