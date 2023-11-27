using UnityEngine;

namespace _Scripts
{
    public class KeyTile : Tile
    {
        public override void DoAction(Pawn player)
        {
            Debug.Log("KeyTile Action");
            
            player.AddKeys(Constants.KEYS_TO_ADD_AT_KEYS_TILE);
        }
    }
}