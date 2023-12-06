using System.Collections;
using UnityEngine;

public class HealingTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();
        player.AddHealth(Constants.TILE_ADD_HEALTH);
        
        yield return new WaitForSeconds(2f);
        HideVFX();
    }
}