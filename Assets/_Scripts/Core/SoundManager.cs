using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TreasureTile.OnTreasureOpen += TreasureTile_OnTreasureOpen;
        TreasureTile.OnTreasureIgnore += TreasureTile_OnTreasureIgnore;
        TreasureTile.OnTreasureNotEnoughKeys += TreasureTile_OnTreasureNotEnoughKeys;
        TreasureTile.OnConfetti += TreasureTile_OnConfetti;
        SkullTile.OnSkullTileDamage += SkullTile_OnSkullTileDamage;
        RespawnTile.OnRespawn += RespawnTile_OnRespawn;
        KeyTile.OnAddKeys += KeyTile_OnAddKeys;
        HealingTile.OnHeal += HealingTile_OnHeal;
        QuestionTile.OnQuizStart += QuestionTile_OnQuizStart;
        
        Pawn.OnCorrectAnswered += Pawn_OnCorrectAnswered;
        Pawn.OnWrongAnswered += Pawn_OnWrongAnswered;
        //Pawn.OnDiceRoll += Pawn_OnDiceRoll;
        Pawn.OnDeathGlobal += Pawn_OnDeathGlobal;
        
        Dice.OnConfetti += Dice_OnConfetti;
        Dice.OnDiceHit += Dice_OnDiceHit;
        // DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        // DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        // CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        // Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        // BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        // TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void QuestionTile_OnQuizStart(object sender, EventArgs e)
    {
        QuestionTile questionTile = sender as QuestionTile;
        PlaySound(_audioClipRefsSO.quizStart, questionTile.transform.position);
    }

    private void Pawn_OnDeathGlobal(object sender, EventArgs e)
    {
        Pawn pawn = sender as Pawn;
        PlaySound(_audioClipRefsSO.death, pawn.transform.position);
    }

    private void Dice_OnDiceHit(object sender, EventArgs e)
    {
        Dice dice = sender as Dice;
        PlaySound(_audioClipRefsSO.diceRollHit, dice.transform.position, 0.15f);
    }

    private void Dice_OnConfetti(object sender, EventArgs e)
    {
        Dice dice = sender as Dice;
        PlaySound(_audioClipRefsSO.confetti, dice.transform.position);
    }

    private void Pawn_OnDiceRoll(object sender, EventArgs e)
    {
        Pawn pawn = sender as Pawn;
        PlaySound(_audioClipRefsSO.diceRollHit, pawn.transform.position, 0.5f);
    }

    private void HealingTile_OnHeal(object sender, EventArgs e)
    {
        HealingTile healingTile = sender as HealingTile;
        PlaySound(_audioClipRefsSO.healingTileOnHeal, healingTile.transform.position);
    }

    private void KeyTile_OnAddKeys(object sender, EventArgs e)
    {
        KeyTile keyTile = sender as KeyTile;
        PlaySound(_audioClipRefsSO.keyTileAddKeys, keyTile.transform.position);
    }

    private void RespawnTile_OnRespawn(object sender, EventArgs e)
    {
        RespawnTile respawnTile = sender as RespawnTile;
        PlaySound(_audioClipRefsSO.respawnTileRespawn, respawnTile.transform.position);
    }

    private void SkullTile_OnSkullTileDamage(object sender, EventArgs e)
    {
        SkullTile skullTile = sender as SkullTile;
        PlaySound(_audioClipRefsSO.skullTileDamage, skullTile.transform.position);
    }

    private void Pawn_OnCorrectAnswered(object sender, EventArgs e)
    {
        Pawn pawn = sender as Pawn;
        PlaySound(_audioClipRefsSO.questionSuccess, pawn.transform.position);
    }

    private void Pawn_OnWrongAnswered(object sender, EventArgs e)
    {
        Pawn pawn = sender as Pawn;
        PlaySound(_audioClipRefsSO.questionFail, pawn.transform.position);
    }
    
    private void TreasureTile_OnConfetti(object sender, EventArgs e)
    {
        TreasureTile treasureTile = sender as TreasureTile;
        PlaySound(_audioClipRefsSO.confetti, treasureTile.transform.position);
    }
    
    private void TreasureTile_OnTreasureNotEnoughKeys(object sender, EventArgs e)
    {
        TreasureTile treasureTile = sender as TreasureTile;
        PlaySound(_audioClipRefsSO.treasureNotEnoughKeys, treasureTile.transform.position);
    }
    
    private void TreasureTile_OnTreasureIgnore(object sender, EventArgs e)
    {
        TreasureTile treasureTile = sender as TreasureTile;
        PlaySound(_audioClipRefsSO.treasureIgnore, treasureTile.transform.position);
    }
    
    private void TreasureTile_OnTreasureOpen(object sender, System.EventArgs e)
    {
        TreasureTile treasureTile = sender as TreasureTile;
        PlaySound(_audioClipRefsSO.treasureOpen, treasureTile.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipRefsSO.footstep, position, volume);
    }
}