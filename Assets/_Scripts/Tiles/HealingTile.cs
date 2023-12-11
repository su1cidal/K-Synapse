using System;
using System.Collections;
using UnityEngine;

public class HealingTile : Tile
{
    public static event EventHandler OnHeal;
    
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();
        
        OnHeal?.Invoke(this, EventArgs.Empty);
        player.AddHealth(Constants.TILE_ADD_HEALTH);
        
        yield return new WaitForSeconds(2f);
        HideVFX();
    }
}