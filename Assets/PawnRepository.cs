using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnRepository : MonoBehaviour
{
    [SerializeField] private List<Pawn> _pawns;
    
    //todo add pawns from list to gameobject as siblings
    //todo replace all [SerializeField] private List<Pawn> to private [SerializeField] PawnRepository

    public void AddPawn(Pawn pawn, bool isPlayer)
    {
        pawn.MakePlayer();
        _pawns.Add(pawn);
    }
    
    public List<Pawn> GetPawns()
    {
        return _pawns;
    }
}
