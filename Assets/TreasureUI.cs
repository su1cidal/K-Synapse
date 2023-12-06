using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureUI : MonoBehaviour
{
    public static TreasureUI Instance { get; private set; }
    
    [SerializeField] private TMP_Text _treasureDescription;
    [SerializeField] private Button _ignore;
    [SerializeField] private Button _open;

    private bool _isEnded = false;
    
    public event Action<Pawn> OnIgnorePressed;
    public event Action<Pawn> OnOpenPressed;
    
    private Pawn player;

    private void Awake()
    {
        Instance = this;
        
        _ignore.GetComponent<Button>().onClick.AddListener(delegate {  });
        _open.GetComponent<Button>().onClick.AddListener(delegate { });
    }
    
    public void SetPlayer(Pawn pawn)
    {
        player = pawn;
    }
    
    private void Start()
    {
        Hide();
    }

    public IEnumerator Show()
    {
        this.gameObject.SetActive(true);
        
        //subscribe to buttons
        // if OPEN is pressed -> take players keys -> give cup -> spawn CONFETTI?
        // if Ignored is pressd -> Hide()
        // handle bots input? WHEN QUESTION TILE WILL CALL THIS we can check if pawn is player
        
        yield return new WaitUntil(IsEnded);
    }
    
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private bool IsEnded()
    {
        return _isEnded;
    }
}
