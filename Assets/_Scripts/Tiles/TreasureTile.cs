using System;
using System.Collections;
using UnityEngine;

public class TreasureTile : Tile
{
    [SerializeField] private GameObject _confetti;

    public override IEnumerator DoAction(Pawn player)
    {
        TreasureUI.Instance.OnOpenPressed += Launch;
        
        ShowVFX();
        
        yield return new WaitForSeconds(1f);
        
        if (player.keys >= Constants.KEYS_TO_OPEN_TREASURE)
        {
            yield return StartCoroutine(TreasureUI.Instance.Show(player));
        }
        else
        {
            Debug.Log("Pawn has not enough keys");
            //todo Show NoKeys emote above player
        }
        
        HideVFX();
    }

    private void Launch()
    {
        StartCoroutine(LaunchConfetti());
    }
    
    private IEnumerator LaunchConfetti()
    {
        _confetti.SetActive(true);
        yield return new WaitForSeconds(5f);
        ResetConfetti();
    }

    private void ResetConfetti()
    {
        _confetti.SetActive(false);
        
        TreasureUI.Instance.OnOpenPressed -= Launch;
    }
}