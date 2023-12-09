using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField] private PawnRepository _pawnRepository;
    [SerializeField] private Map _map;
    
    private void Start()
    {
        var pawns = _pawnRepository.GetPawns();
        
        foreach (var pawn in pawns)
        {
            pawn.OnDeath += OnDeathHappened;
        }
    }

    private void OnDeathHappened(Pawn pawn)
    {
        Debug.Log($"On death happened {pawn.playerName} {pawn.health}");
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
