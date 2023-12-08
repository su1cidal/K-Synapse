using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField] private List<Pawn> _pawns;
    [SerializeField] private Map _map;

    private void Awake()
    {
        foreach (var pawn in _pawns)
        {
            pawn.OnDeath += OnDeathHapped;
        }
    }

    private void OnDeathHapped(Pawn pawn)
    {
        var nearestTile = GetNearestTile(pawn);

        StartCoroutine(MovePawnToRespawnTile(pawn, nearestTile));
    }

    private Tile GetNearestTile(Pawn pawn)
    {
        float minimalDistance = Single.MaxValue;
        Tile minimalTile = _map.GetRespawnTiles()[0];

        foreach (var tile in _map.GetRespawnTiles())
        {
            float distance = Vector3.Distance(tile.transform.position, pawn.transform.position);
            if (distance < minimalDistance)
            {
                minimalDistance = distance;
                minimalTile = tile;
            }
        }

        return minimalTile;
    }

    private IEnumerator MovePawnToRespawnTile(Pawn pawn, Tile nearestTile)
    {
        yield return new WaitForSeconds(2f);
        
        pawn.transform.DOMove(nearestTile.transform.position, 1f);
        pawn.currentMapTile.RemovePawn(pawn);

        pawn.currentMapTile = nearestTile;
        pawn.currentMapTile.AddPawn(pawn);
        
        yield return StartCoroutine(pawn.currentMapTile.DoAction(pawn));
    }
}
