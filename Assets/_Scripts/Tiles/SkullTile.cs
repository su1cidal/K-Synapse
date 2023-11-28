using UnityEngine;

namespace _Scripts.Tiles
{
    public class SkullTile : Tile
    {
        public override bool DoAction(Pawn player)
        {
            Debug.Log("SkullTile Action");
            player.DoDamage(Constants.TILE_DO_DAMAGE);
            
            return true;
        }
    }
}