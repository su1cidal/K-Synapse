using System.Collections;
using UnityEngine;

public class TreasureTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        yield return new WaitForSeconds(1f);
        
        if (player.keys >= Constants.KEYS_TO_OPEN_TREASURE)
        {
            yield return StartCoroutine(TreasureUI.Instance.Show());
        }
        else
        {
            //todo Show NoKeys emote above player
        }
    }
}