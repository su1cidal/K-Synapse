using System.Collections;
using UnityEngine;

public class RespawnTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        yield return new WaitForSeconds(1f); // wait till pawn move to tile
        
        ShowVFX();

        player.Heal();
        
        yield return new WaitForSeconds(2f);
        
        HideVFX();
    }
}