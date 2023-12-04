using System.Collections;
using UnityEngine;

public class HealingTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        Debug.Log("HealingTile Action");
        player.AddHealth(Constants.TILE_ADD_HEALTH);
        
        yield return new WaitForSeconds(1f);
    }
}