using System.Collections;
using UnityEngine;

public class SkullTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();
        player.DoDamage(Constants.TILE_DO_DAMAGE);

        yield return new WaitForSeconds(2f);
        HideVFX();
    }
}