using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayersUI : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettingsSo;
    [SerializeField] private GameObject _playerDataPlaceHolder;

    [SerializeField] private PawnRepository _pawnRepository;
    
    void Start()
    {
        var pawns = _pawnRepository.GetPawns();
        
        for (int i = 0; i < _gameSettingsSo.playerCount; i++)
        {
            var playerGameObject = Instantiate(_playerDataPlaceHolder, this.transform);
            playerGameObject.name = i.ToString();
            
            var playerData = playerGameObject.GetComponent<PlayerData>();
            
            pawns[i].place = i;
            
            playerData.SetPlayer(pawns[i]);
            playerData.OnChangeName += delegate { Sort(this.transform); };
        }
    }

    private void Sort(Transform current)
    {
        IOrderedEnumerable<Transform> orderedChildren = current.Cast<Transform>().OrderByDescending(tr => Number(tr.name));
 
        #if UNITY_EDITOR
        foreach (Transform child in orderedChildren)
        {
            Undo.SetTransformParent(child, null, "Reorder children");
            Undo.SetTransformParent(child, current, "Reorder children");
        }
        #endif

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
