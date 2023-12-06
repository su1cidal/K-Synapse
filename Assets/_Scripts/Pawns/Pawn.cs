using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public PlayerMaterialsSO materials;
    
    [SerializeField] private bool _isPlayer;
    
    private bool _isWalking;
    
    public bool IsAnswered = false;

    public event Action OnHealthChanged;
    public event Action OnKeysChanged;
    public event Action OnCupsChanged;
    
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
    
    public bool IsPlayer()
    {
        return _isPlayer;
    }

    public void SetIsWalking(bool value)
    {
        _isWalking = value;
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
        }
        else
        {
            health -= amount;
        }
        OnHealthChanged?.Invoke();
    }

    public void AddKeys(int amount)
    {
        keys += amount;
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
        OnKeysChanged?.Invoke();
    }
    
    public void AddCups(int amount)
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

    public int RollADice() //todo export to personal class
    {
        rolledDice = Random.Range(Constants.ROLL_MIN_VALUE, Constants.ROLL_MAX_VALUE + 1);
        return rolledDice;
    }

    private IEnumerator ShowRolledDice()
    {
        //todo realise it

        yield return new WaitForSeconds(1f);
    }

    public void FixPosition()
    {
        visual.transform.position = new Vector3(0,0,0);
    }
}