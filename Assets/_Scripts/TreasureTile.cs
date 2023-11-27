using UnityEngine;

namespace _Scripts
{
    public class TreasureTile : Tile
    {
        public override void DoAction(Pawn player)
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
        }
    }
}