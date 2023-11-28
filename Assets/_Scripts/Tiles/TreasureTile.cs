using UnityEngine;

namespace _Scripts.Tiles
{
    public class TreasureTile : Tile
    {
        public override bool DoAction(Pawn player)
        {
            Debug.Log("TreasureTile Action");
            if (player.keys >= Constants.KEYS_TO_OPEN_TREASURE)
            {
                //todo Show treasure interaction window
            }
            else
            {
                //todo Show NoKeys emote above player
            }
            
            return true;
        }
    }
}