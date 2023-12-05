using TMPro;
using UnityEngine;

public class CupsToWinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _cupsCount;

    public void SetCupsCount(int value)
    {
        _cupsCount.text = value.ToString();
    }
}
