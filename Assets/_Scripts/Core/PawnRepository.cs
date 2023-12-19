using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnRepository : MonoBehaviour
{
    [SerializeField] private GameObject _pawnPrefab;
    [SerializeField] private List<PlayerMaterialsSO> _playerMaterialsSO;
    
    [SerializeField] private List<Pawn> _pawns;

    public void AddPawn(Pawn pawn)
    {
        _pawns.Add(pawn);
    }
    
    public List<Pawn> GetPawns()
    {
        return _pawns;
    }
    
    public void InitializePawns(GameSettingsSO gameSettingsSO)
    {
        AddPawn(CreatePlayerPawn(gameSettingsSO));
        
        for (int i = 0; i < gameSettingsSO.playerCount-1; i++)
        {
            AddPawn(CreateBotPawn(gameSettingsSO));
        }
    }

    private Pawn CreateBotPawn(GameSettingsSO gameSettingsSO)
    {
        var newPawn = Instantiate(_pawnPrefab);
            
        ChangeBotName(newPawn);

        ChangeBotMaterial(gameSettingsSO, newPawn);
        
        return newPawn.GetComponent<Pawn>();
    }

    private void ChangeBotMaterial(GameSettingsSO gameSettingsSO, GameObject newPawn)
    {
        bool isSimilar = false;
        
        var randomIndex = Random.Range(0, _playerMaterialsSO.Count);
        var randomMaterials = _playerMaterialsSO[randomIndex];
        
        foreach (var pawn in _pawns)
        {
            if (randomMaterials == pawn.materials)
                isSimilar = true;
        }

        if (isSimilar)
            ChangeBotMaterial(gameSettingsSO, newPawn);
        else newPawn.GetComponent<Pawn>().materials = _playerMaterialsSO[randomIndex];
    }

    private void ChangeBotName(GameObject newPawn)
    {
        bool isSimilar = false;
        
        var randomIndex = Random.Range(0, BotNames.Count);
        var name = BotNames.ElementAt(randomIndex).Value;
            
        foreach (var pawn in _pawns)
        {
            if (name == pawn.playerName)
                isSimilar = true;
        }

        if (isSimilar)
            ChangeBotName(newPawn);
        else
            newPawn.GetComponent<Pawn>().playerName = name;
    }
    
    private Pawn CreatePlayerPawn(GameSettingsSO gameSettingsSO)
    {
        var player = Instantiate(_pawnPrefab);
        
        player.GetComponent<Pawn>().playerName = gameSettingsSO.playerName;
        player.GetComponent<Pawn>().materials = gameSettingsSO.playerMaterials;
        
        player.GetComponent<Pawn>().MakePlayer();

        return player.GetComponent<Pawn>();
    }

    public Dictionary<int, string> BotNames = new Dictionary<int, string>()
    {
        { 1, "Andrew Bot"},
        { 2, "Omega Bot"},
        { 3, "Xavier Bot"},
        { 4, "Luna Bot"},
        { 5, "Griffin Bot"},
        { 6, "Serafina Bot"},
        { 7, "Phoenix Bot"},
        { 8, "Titan Bot"},
        { 9, "Aria Bot"},
        { 10, "Zephyr Bot"},
        { 11, "Astrid Bot"},
        { 12, "Draco Bot"},
        { 13, "Nova Bot"},
        { 14, "Blaze Bot"},
        { 15, "Luna Bot"},
        { 16, "Atlas Bot"},
        { 17, "Orion Bot"},
        { 18, "Celeste Bot"},
        { 19, "Dorian Bot"},
        { 20, "Echo Bot"},
        { 21, "Vortex Bot"},
        { 22, "Pegasus Bot"},
        { 23, "Catalyst Bot"},
        { 24, "Zara Bot"},
        { 25, "Spartan Bot"},
        { 26, "Nebula Bot"},
        { 27, "Quantum Bot"},
        { 28, "Eclipse Bot"},
        { 29, "Havoc Bot"},
        { 30, "Typhoon Bot"},
        { 31, "Zephyr Bot"},
        { 32, "Aether Bot"},
        { 33, "Solaris Bot"},
        { 34, "Crimson Bot"},
        { 35, "Vivid Bot"},
        { 36, "Galaxy Bot"},
        { 37, "Dynamo Bot"},
        { 38, "Astral Bot"},
        { 39, "Tempest Bot"},
        { 40, "Zodiac Bot"},
        { 41, "Serenity Bot"},
        { 42, "Quantum Pulse Bot"},
        { 43, "Apex Bot"},
        { 44, "Zenith Bot"},
        { 45, "Cosmic Bot"},
        { 46, "Inferno Bot"},
        { 47, "Radiant Bot"},
        { 48, "Epoch Bot"},
        { 49, "Aegis Bot"},
        { 50, "Spectra Bot"},
        { 51, "Lumina Bot"},
        { 52, "Stratos Bot"},
        { 53, "Vesper Bot"},
        { 54, "Elysium Bot"},
        { 55, "Phantom Bot"},
        { 56, "Nyx Bot"},
        { 57, "Ignis Bot"},
        { 58, "Veritas Bot"},
        { 59, "Sapphire Bot"},
        { 60, "Pulse Bot"},
        { 61, "Galactic Bot"},
        { 62, "Cinder Bot"},
        { 63, "Aurora Bot"},
        { 64, "Equinox Bot"},
        { 65, "Mirage Bot"},
        { 66, "Solstice Bot"},
        { 67, "Quasar Bot"},
        { 68, "Eclipse Bot"},
        { 69, "Rogue Bot"},
        { 70, "Nebula Bot"},
        { 71, "Helios Bot"},
        { 72, "Zenith Bot"},
        { 73, "Astro Bot"},
        { 74, "Vortex Bot"},
        { 75, "Sylvan Bot"},
        { 76, "Nimbus Bot"},
        { 77, "Paragon Bot"},
        { 78, "Sphinx Bot"},
        { 79, "Horizon Bot"},
        { 80, "Aether Bot"}
    };
}