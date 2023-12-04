using System.Collections;
using UnityEngine;

public class RespawnTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        Debug.Log("RespawnTile Action");
        // todo create cool respawn effect

        yield return new WaitForSeconds(1f);
    }
}