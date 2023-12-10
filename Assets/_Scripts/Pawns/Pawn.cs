using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pawn : MonoBehaviour
{
    [SerializeField] public string playerName;
    [SerializeField] public int health = (int)Constants.PLAYER_MAX_HEALTH;
    [SerializeField] public int keys = 0;
    [SerializeField] public int cups = 0;
    [SerializeField] public int place = 0;
    [Header("Collectables")]
    [SerializeField] public int damageReceived = 0;
    [SerializeField] public int keysGained = 0;
    [SerializeField] public int keysLost = 0;
    [SerializeField] public List<Question> correctAnswered;
    [SerializeField] public List<Question> wrongAnswered;
    [Header("   ")]
    [SerializeField] public int rolledDice = -1;
    [SerializeField] public Tile currentMapTile;
    [Header("   ")]
    [SerializeField] public GameObject visual;
    [SerializeField] public GameObject model;
    [SerializeField] public PlayerMaterialsSO materials;
    [SerializeField] private Dice _dice;
    [SerializeField] private GameObject _noKeys;
    
    [SerializeField] private bool _isPlayer = false;
    
    private bool _isWalking;
    
    public bool IsAnswered = false;

    public event Action OnHealthChanged;
    public event Action<Pawn> OnDeath;
    public event Action OnKeysChanged;
    public event Action OnCupsChanged;
    public event Action OnJump;

    public Pawn(string playerName, PlayerMaterialsSO materials, bool isPlayer)
    {
        this.playerName = playerName;
        this.materials = materials;
        
        if(isPlayer) MakePlayer();
    }
    
    private void Start()
    {
        if (materials.PlayerColor != null)
        {
            var modelMeshRenderer = model.GetComponent<MeshRenderer>();
            modelMeshRenderer.material = materials.PlayerColor;
        }
    }

    public IEnumerator ShowNoKeys()
    {
        _noKeys.SetActive(true);
        yield return new WaitForSeconds(2f);
        _noKeys.SetActive(false);
    }
    
    public IEnumerator RollADice()
    {
        yield return StartCoroutine(_dice.Roll(this));
    }
    
    public Material GetMaterialUI()
    {
        if (materials.PlayerAnswer == null)
        {
            return null;
        }

        return materials.PlayerAnswer;
    }
    
    public bool IsWalking()
    {
        return _isWalking;
    }

    public void MakeJump()
    {
        OnJump?.Invoke();
    }

    public bool IsPlayer => _isPlayer;

    public void MakePlayer()
    {
        _isPlayer = true;
    }
    
    public void SetIsWalking(bool value)
    {
        _isWalking = value;
    }

    public void Heal()
    {
        health = (int)Constants.PLAYER_MAX_HEALTH;
        OnHealthChanged?.Invoke();
    }
    
    public void AddHealth(int amount)
    {
        if (health + amount >= Constants.PLAYER_MAX_HEALTH)
        {
            health = (int)Constants.PLAYER_MAX_HEALTH;
        }
        else
        {
            health += amount;
        }
        OnHealthChanged?.Invoke();
    }
    
    public void DoDamage(int amount)
    {
        if (health - amount <= Constants.PLAYER_MIN_HEALTH)
        {
            health = (int)Constants.PLAYER_MIN_HEALTH;
            OnDeath?.Invoke(this);
        }
        else
        {
            health -= amount;
        }

        damageReceived += amount;
        OnHealthChanged?.Invoke();
    }

    public void AddKeys(int amount)
    {
        keys += amount;
        
        keysGained += amount;
        OnKeysChanged?.Invoke();
    }
    
    public void RemoveKeys(int amount)
    {
        if (keys - amount <= Constants.PLAYER_MIN_KEYS)
        {
            keys = Constants.PLAYER_MIN_KEYS;
        }
        else
        {
            keys -= amount;
        }

        keysLost += amount;
        OnKeysChanged?.Invoke();
    }
    
    public void AddCup()
    {
        cups += Constants.CUPS_TO_ADD_AT_TREASURE_TILE;
        OnCupsChanged?.Invoke();
    }
    
    public void RemoveCups(int amount)
    {
        if (cups - amount <= Constants.CUPS_TO_ADD_AT_TREASURE_TILE)
        {
            cups = Constants.CUPS_TO_ADD_AT_TREASURE_TILE;
        }
        else
        {
            cups -= amount;
        }
        OnCupsChanged?.Invoke();
    }

    public void FixPosition()
    {
        var resetVector = new Vector3(0,0,0);
        visual.transform.DOLocalMove(resetVector, 0.5f);
    }
}