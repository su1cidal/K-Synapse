using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefsSO", menuName = "SO/AudioClipRefsSO")]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] questionFail;
    public AudioClip[] questionsSuccess;
    public AudioClip[] footstep;
    public AudioClip[] warning;
}