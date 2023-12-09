using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ScoreboardUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private GameObject _entriesPlaceHolder;
    [SerializeField] private GameObject _playerEntry;
    [SerializeField] private PawnRepository _pawnRepository;

    public void Show()
    {
        Clear();
        FillScoreboard(_pawnRepository.GetPawns());
        _UI.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        _UI.gameObject.SetActive(false);
    }

    private void Clear()
    {
        foreach (Transform child in _UI.transform)
        {
            if (child.TryGetComponent(out PlayerEntryUI playerEntry))
            {
                Destroy(playerEntry.gameObject);
            }
            
        }
    }
    
    private void FillScoreboard(List<Pawn> pawns)
    {
        foreach (var pawn in pawns)
        {
            GameObject newEntry = Instantiate(_playerEntry, _entriesPlaceHolder.transform);
            
            var playerEntry = newEntry.GetComponent<PlayerEntryUI>();
            
            playerEntry.SetImageBackground(pawn.GetMaterialUI());
            playerEntry.SetName(pawn.playerName);
            playerEntry.SetCorrectAnswers(pawn.correctAnswered.Count.ToString());
            playerEntry.SetWrongAnswers(pawn.wrongAnswered.Count.ToString());
            playerEntry.SetDamageReceived(pawn.damageReceived.ToString());
            playerEntry.SetKeysGained(pawn.keysGained.ToString());
            playerEntry.SetKeysLost(pawn.keysLost.ToString());

            newEntry.name = $"{pawn.correctAnswered.Count}";
        }
        
        Sort(_entriesPlaceHolder.transform);
    }

    private void Sort(Transform current)
    {
        IOrderedEnumerable<Transform> orderedChildren = current.Cast<Transform>().OrderByDescending(tr => Number(tr.name));
    
        foreach (Transform child in orderedChildren)
        {
            Undo.SetTransformParent(child, null, "Reorder children");
            Undo.SetTransformParent(child, current, "Reorder children");
        }
    }
    
    private int Number(string str) 
    { 
        int result_ignored;
        if (int.TryParse(str,out result_ignored))
            return result_ignored;
    
        else 
            return 0;
    }
}
