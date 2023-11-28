using UnityEngine;

public class KeyTile : Tile
{
    public override bool DoAction(Pawn player)
    {
        Debug.Log("KeyTile Action");

        player.AddKeys(Constants.KEYS_TO_ADD_AT_KEYS_TILE);



        return true;
    }
}