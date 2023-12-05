using UnityEngine;
using Random = UnityEngine.Random;

public class Pawn : MonoBehaviour
{
    [SerializeField] public int health = Constants.PLAYER_MAX_HEALTH;
    [SerializeField] public int keys = 0;
    [SerializeField] public int rolledDice = -1;
    [SerializeField] public Tile currentMapTile;
    [Header("   ")]
    [SerializeField] public Question[] correctAnswered;
    [SerializeField] public Question[] wrongAnswered;
    [Header("   ")]
    [SerializeField] public PlayerMaterialsSO materials;
    [SerializeField] public GameObject visual;
    [SerializeField] private bool _isPlayer;

    private int _lastPositionEdit;
    private bool _isWalking;
    public bool IsAnswered = false;

    public Material GetAnswerMaterial()
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

    public void DoDamage(int amount)
    {
        if (health - amount <= Constants.PLAYER_MIN_HEALTH)
        {
            health = Constants.PLAYER_MIN_HEALTH;
        }
        else
        {
            health -= amount;
        }
    }

    public void AddHealth(int amount)
    {
        if (health + amount >= Constants.PLAYER_MAX_HEALTH)
        {
            health = Constants.PLAYER_MAX_HEALTH;
        }
        else
        {
            health += amount;
        }
    }

    public void AddKeys(int amount)
    {
        keys += amount;
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
    }

    public int RollADice() //todo export to personal class
    {
        rolledDice = Random.Range(Constants.ROLL_MIN_VALUE, Constants.ROLL_MAX_VALUE + 1);
        return rolledDice;
    }

    public void SetLastPositionEdit(int editValue) => _lastPositionEdit = editValue;

    public int GetLastPositionEdit() => _lastPositionEdit;
}