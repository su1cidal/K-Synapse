using System;
using System.Collections;
using UnityEngine;

public class SkullTile : Tile
{
    public static event EventHandler OnSkullTileDamage;
    
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();
        
        OnSkullTileDamage?.Invoke(this, EventArgs.Empty);
        player.DoDamage(Constants.TILE_DO_DAMAGE);

        yield return new WaitForSeconds(2f);
        HideVFX();
    }
}