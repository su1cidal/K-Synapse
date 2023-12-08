using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TreasureUI : MonoBehaviour
{
    public static TreasureUI Instance { get; private set; }
    
    [SerializeField] private GameObject _UI;
    [SerializeField] private Button _ignore;
    [SerializeField] private Button _open;
    [SerializeField] private TMP_Text _keysCount;

    private List<SelectButton> _selectButtonList;
    private bool _isEnded = false;
    private Pawn _player;
    
    public event Action OnIgnorePressed;
    public event Action OnOpenPressed;
    
    public delegate void SelectButton(Pawn pawn);

    private void Awake()
    {
        Instance = this;

        _selectButtonList = new List<SelectButton>();
        
        _selectButtonList.Add(Open);
        _selectButtonList.Add(Ignore);

        _keysCount.text = $"x{Constants.KEYS_TO_OPEN_TREASURE}";
        
        _ignore.GetComponent<Button>().onClick.AddListener(delegate { Ignore(_player); });
        _open.GetComponent<Button>().onClick.AddListener(delegate { Open(_player); });
    }
    
    public void SetPlayer(Pawn pawn)
    {
        _player = pawn;
    }
    
    private void Start()
    {
        Hide();
    }

    public IEnumerator Show(Pawn pawn)
    {
        _isEnded = false;

        if (!pawn.IsPlayer)
        {
            BlockUI();
            StartCoroutine(SelectForBot(pawn));
        }
        if(pawn.IsPlayer)
        {
            SetPlayer(pawn);
        }
        
        _UI.gameObject.SetActive(true);
        
        yield return new WaitUntil(IsEnded);
    }

    private IEnumerator SelectForBot(Pawn pawn)
    {
        yield return new WaitForSeconds(1);
        float waitTime = Random.Range(1, 5f);
        int choose = Random.Range(1, 5);
        yield return new WaitForSeconds(waitTime);

        switch (choose)
        {
            case 1:
                Open(pawn);
                break;
            case 2:
                Open(pawn);
                break;
            case 3:
                Open(pawn);
                break;
            case 4:
                Open(pawn);
                break;
            case 5:
                Ignore(pawn);
                break;
            default:
                Ignore(pawn);
                break;
        }
    }

    private void BlockUI()
    {
        _ignore.interactable = false;
        _open.interactable = false;
    }
    
    private void UnblockUI()
    {
        _ignore.interactable = true;
        _open.interactable = true;
    }

    private void Open(Pawn pawn)
    {
        pawn.RemoveKeys(Constants.KEYS_TO_OPEN_TREASURE);
        pawn.AddCup();
        
        _isEnded = true;
        Hide();

        OnOpenPressed?.Invoke();
    }
    
    private void Ignore(Pawn pawn)
    {
        _isEnded = true;
        Hide();
        
        OnIgnorePressed?.Invoke();
    }
    
    public void Hide()
    {
        _UI.gameObject.SetActive(false);
        UnblockUI();
    }

    private bool IsEnded()
    {
        return _isEnded;
    }
}
