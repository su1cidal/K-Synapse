using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] public List<Pawn> pawnsOnTile;
    [SerializeField] public List<Tile> adjacentTiles;
    
    [SerializeField] private GameObject _vfx;

    public abstract IEnumerator DoAction(Pawn player);

    private void Start()
    {
        HideVFX();
    }

    public void ShowVFX()
    {
        _vfx.SetActive(true);
    }
    
    public void HideVFX()
    {
        _vfx.SetActive(false);
    }
    
    public void AddPawn(Pawn pawn)
    {
        if (!pawnsOnTile.Contains(pawn))
        {
            pawnsOnTile.Add(pawn);

            if (pawnsOnTile.Count >= 2)
            {
                OnPawnFinalMove(pawn);
            }
        }
    }

    public void RemovePawn(Pawn pawn)
    {
        if (pawnsOnTile.Contains(pawn))
        {
            pawn.FixPosition();
            pawnsOnTile.Remove(pawn);
        }
    }

    public void OnPawnFinalMove(Pawn pawn)
    {
        if (pawnsOnTile.Count < 2) return;

        Vector3 resetVector;
        var rand = new Random();
        var move = rand.Next(0, 7);
        
        switch (move)
        {
            case 0:
                resetVector = new Vector3(+Constants.PAWN_OFFSET, 0f, +Constants.PAWN_OFFSET);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
            case 1:
                resetVector = new Vector3(+Constants.PAWN_OFFSET, 0f, -Constants.PAWN_OFFSET);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
            case 2:
                resetVector = new Vector3(-Constants.PAWN_OFFSET, 0f, +Constants.PAWN_OFFSET);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
            case 3:
                resetVector = new Vector3(-Constants.PAWN_OFFSET, 0f, -Constants.PAWN_OFFSET);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
            case 4:
                resetVector = new Vector3(+Constants.PAWN_OFFSET, 0f, 0);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
            case 5:
                resetVector = new Vector3(0, 0f, -Constants.PAWN_OFFSET);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
            case 6:
                resetVector = new Vector3(0, 0f, +Constants.PAWN_OFFSET);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
            case 7:
                resetVector = new Vector3(-Constants.PAWN_OFFSET, 0f, 0);
                pawn.visual.transform.DOLocalMove(resetVector, 0.5f);
                break;
        }
        
    }
}