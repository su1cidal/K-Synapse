using System.Collections;
using UnityEngine;

public class EmptyTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        Debug.Log("EmptyTile Action");
        
        yield return new WaitForSeconds(1f);
    }
}