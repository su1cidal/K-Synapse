using UnityEngine;

public class StartTile : Tile
{
    public override bool DoAction(Pawn player)
    {
        Debug.Log("StartTile Action!");

        return true;
    }
}