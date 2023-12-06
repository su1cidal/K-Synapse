using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardUI : MonoBehaviour
{
    [SerializeField] private GameObject _playerEntry;
    [SerializeField] private List<Pawn> _pawns;

    public void Show()
    {
        FillScoreboard(_pawns);
        this.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        this.gameObject.SetActive(false);
        Clear();
    }

    private void Clear()
    {
        foreach (Transform child in this.transform)
        {
            if (child.TryGetComponent<PlayerEntryUI>(out PlayerEntryUI playerEntry))
            {
                GameObject.Destroy(playerEntry.gameObject);
            }
            
        }
    }
    
    private void FillScoreboard(List<Pawn> pawns)
    {
        foreach (var pawn in pawns)
        {
            GameObject newEntry = Instantiate(_playerEntry, this.transform);
            
            var playerEntry = newEntry.GetComponent<PlayerEntryUI>();
            
            playerEntry.SetImageBackground(pawn.GetMaterialUI());
            playerEntry.SetName(pawn.playerName);
            playerEntry.SetCorrectAnswers(pawn.correctAnswered.Count.ToString());
            playerEntry.SetWrongAnswers(pawn.wrongAnswered.Count.ToString());
            playerEntry.SetDamageReceived(pawn.damageReceived.ToString());
            playerEntry.SetKeysGained(pawn.keysGained.ToString());
            playerEntry.SetKeysLost(pawn.keysLost.ToString());
        }
    }
}
