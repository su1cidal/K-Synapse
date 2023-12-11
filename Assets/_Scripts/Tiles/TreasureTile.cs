using System;
using System.Collections;
using UnityEngine;

public class TreasureTile : Tile
{
    [SerializeField] private GameObject _confetti;

    public static event EventHandler OnTreasureOpen;
    public static event EventHandler OnTreasureIgnore;
    public static event EventHandler OnTreasureNotEnoughKeys;
    public static event EventHandler OnConfetti;
    
    public override IEnumerator DoAction(Pawn player)
    {
        TreasureUI.Instance.OnOpenPressed += Launch;
        TreasureUI.Instance.OnIgnorePressed += Ignore;
        
        ShowVFX();
        
        if (player.keys >= Constants.KEYS_TO_OPEN_TREASURE)
        {
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(TreasureUI.Instance.Show(player));
        }
        else
        {
            OnTreasureNotEnoughKeys?.Invoke(this, EventArgs.Empty);
            Debug.Log("Pawn has not enough keys");
            yield return StartCoroutine(player.ShowNoKeys());
        }
        
        HideVFX();
    }

    private void Launch()
    {
        OnTreasureOpen?.Invoke(this, EventArgs.Empty);
        StartCoroutine(LaunchConfetti());
    }
    
    private void Ignore()
    {
        OnTreasureIgnore?.Invoke(this, EventArgs.Empty);
    }
    
    private IEnumerator LaunchConfetti()
    {
        yield return new WaitForSeconds(2f);
        _confetti.SetActive(true);
        OnConfetti?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(5f);
        ResetConfetti();
    }

    private void ResetConfetti()
    {
        _confetti.SetActive(false);
        
        TreasureUI.Instance.OnOpenPressed -= Launch;
        TreasureUI.Instance.OnIgnorePressed -= Launch;
    }
}