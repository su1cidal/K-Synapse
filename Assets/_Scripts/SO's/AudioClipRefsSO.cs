using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefsSO", menuName = "SO/AudioClipRefsSO")]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] questionFail;
    public AudioClip[] questionSuccess;
    public AudioClip[] footstep;
    public AudioClip[] death;
    
    public AudioClip[] treasureOpen;
    public AudioClip[] treasureIgnore;
    public AudioClip[] treasureNotEnoughKeys;
    
    public AudioClip[] skullTileDamage;
    
    public AudioClip[] respawnTileRespawn;
    
    public AudioClip[] keyTileAddKeys;
    
    public AudioClip[] healingTileOnHeal;
    
    public AudioClip[] quizStart;
    
    public AudioClip[] diceRollHit;
    
    public AudioClip[] confetti;
}