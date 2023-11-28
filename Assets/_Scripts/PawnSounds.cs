using UnityEngine;

public class PawnSounds : MonoBehaviour
{
    [SerializeField] private float volume = 0.5f;
    [SerializeField] private float _footstepTimerMax = 0.1f;
    private Pawn _pawn;
    private float _footstepTimer;

    private void Awake()
    {
        _pawn = GetComponent<Pawn>();
    }

    private void Update()
    {
        _footstepTimer -= Time.deltaTime;
        if (_footstepTimer < 0f)
        {
            _footstepTimer = _footstepTimerMax;
            if (_pawn.IsWalking())
            {
                SoundManager.Instance.PlayFootsetpsSound(_pawn.transform.position, volume);
            }
        }
    }
}