using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayersUI : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettingsSo;
    [SerializeField] private GameObject _playerDataPlaceHolder;
    
    [SerializeField] private List<Pawn> _pawns;
    
    void Start()
    {
        for (int i = 0; i < _gameSettingsSo.playerCount; i++)
        {
            var playerGameObject = Instantiate(_playerDataPlaceHolder, this.transform);
            playerGameObject.name = i.ToString();
            
            var playerData = playerGameObject.GetComponent<PlayerData>();
            
            _pawns[i].place = i;
            
            playerData.SetPlayer(_pawns[i]);
            playerData.OnChangeName += delegate { Sort(this.transform); };
        }
    }

    private void Sort(Transform current)
    {
        IOrderedEnumerable<Transform> orderedChildren = current.Cast<Transform>().OrderByDescending(tr => Number(tr.name));
 
        foreach (Transform child in orderedChildren)
        {
            Undo.SetTransformParent(child, null, "Reorder children");
            Undo.SetTransformParent(child, current, "Reorder children");
        }

        foreach (Transform child in orderedChildren)
        {
            child.GetComponent<PlayerData>().UpdatePlace();
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
