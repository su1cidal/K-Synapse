using TMPro;
using UnityEngine;

public class TurnCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _turnCount;

    public void SetTurnCount(int turnCount, int maxTurnCount)
    {
        string turnCountText = $"{turnCount}/{maxTurnCount}";
        _turnCount.text = turnCountText;
    }
}