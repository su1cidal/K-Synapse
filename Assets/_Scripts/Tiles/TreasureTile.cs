using System.Collections;
using UnityEngine;

public class TreasureTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        Debug.Log("TreasureTile Action");
        if (player.keys >= Constants.KEYS_TO_OPEN_TREASURE)
        {
            //todo Show treasure interaction window
            // Only if PLAYER is on it
        }
        else
        {
            //todo Show NoKeys emote above player
        }

        yield return new WaitForSeconds(1f);
    }
}