using System.Collections;
using UnityEngine;

public class RespawnTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();
        // todo create cool respawn effect

        yield return new WaitForSeconds(2f);
        HideVFX();
    }
}