using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour
{
    [SerializeField] private GameObject _playerVisual;
    [SerializeField] private GameObject _turns;

    public void SetTurnOrder(List<Pawn> pawns)
    {
        Clear();

        foreach (var pawn in pawns)
        {
            GameObject newIcon = Instantiate(_playerVisual, _turns.transform);
        
            var image = newIcon.GetComponent<Image>();
            var material = pawn.GetMaterialUI();
            image.material = material;
        }
    }

    private void Clear()
    {
        foreach (Transform child in _turns.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}