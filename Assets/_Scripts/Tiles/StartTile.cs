using System.Collections;
using UnityEngine;

public class StartTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        Debug.Log("StartTile Action!");

        yield return new WaitForSeconds(1f);
    }
}