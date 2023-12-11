using System;
using System.Collections;
using UnityEngine;

public class RespawnTile : Tile
{
    public static event EventHandler OnRespawn;
    
    public override IEnumerator DoAction(Pawn player)
    {
        yield return new WaitForSeconds(1f); // wait till pawn move to tile
        
        ShowVFX();

        OnRespawn?.Invoke(this,EventArgs.Empty);
        player.Heal();
        
        yield return new WaitForSeconds(2f);
        
        HideVFX();
    }
}