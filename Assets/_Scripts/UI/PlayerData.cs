using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Image _playerColorImage;
    [SerializeField] private TMP_Text _playerText;
    [SerializeField] private TMP_Text _playerNumber;
    
    [Header(" ")]
    [SerializeField] private TMP_Text _playerKeys;
    [SerializeField] private TMP_Text _playerCups;
    
    [Header(" ")]
    [SerializeField] private Image _healthImage;
    [SerializeField] private float _healthNormalized;
    [SerializeField] private TMP_Text _healthText;
    
    [Header(" ")]
    [SerializeField] private Pawn _player;

    private int _cupMultiplierToBalanceLeaderboard = 1000;

    public event Action OnChangeName;

    public void SetPlayer (Pawn pawn)
    {
        _player = pawn;

        SetPlayerStats();
        SubscribeToEvents();
    }

    private void ChangeSelfName()
    {
        this.name = (_player.keys + _player.cups * _cupMultiplierToBalanceLeaderboard).ToString();

        OnChangeName?.Invoke();
    }

    public void UpdatePlace()
    {
        _player.place = this.transform.GetSiblingIndex()+1;
        _playerNumber.text = $"#{_player.place}";
    }

    private void SetHealthNormalized(float health)
    {
        _healthImage.transform.localScale = new Vector3(health,1,1);
    }

    private void SetPlayerStats()
    {
        _playerColorImage.material = _player.GetMaterialUI();
        _playerText.text = _player.playerName;
        _playerNumber.text = $"#{_player.place+1}";
    }

    private void OnPlayerKeysChanged()
    {
        _playerKeys.text = _player.keys.ToString();
        ChangeSelfName();
    }

    private void OnPlayerCupsChanged()
    {
        _playerCups.text = _player.cups.ToString();
        ChangeSelfName();
    }

    private void OnPlayerHealthChanged()
    {
        var health = _player.health;
        _healthText.text = health.ToString();


        _healthNormalized = GetNormalizedHealth(health, Constants.PLAYER_MIN_HEALTH, Constants.PLAYER_MAX_HEALTH);
        
        SetHealthNormalized(_healthNormalized);
    }

    private float GetNormalizedHealth(int health, float min, float max)
    {
        float normalized = (health - min) /
                           (max - min);
        
        return normalized;
    }
    
    private void SubscribeToEvents()
    {
        _player.OnCupsChanged += OnPlayerCupsChanged;
        _player.OnHealthChanged += OnPlayerHealthChanged;
        _player.OnKeysChanged += OnPlayerKeysChanged;
    }
}
