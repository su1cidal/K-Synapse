using System.Collections;
using UnityEngine;

public class KeyTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();

        player.AddKeys(Constants.KEYS_TO_ADD_AT_KEYS_TILE);

        yield return new WaitForSeconds(2f);
        HideVFX();
    }
}