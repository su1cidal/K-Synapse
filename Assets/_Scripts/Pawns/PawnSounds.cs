using UnityEngine;

public class PawnSounds : MonoBehaviour
{
    [SerializeField] private float _volume = 0.7f;
    [SerializeField] private float _footstepTimerMax = 0.605f;
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
                SoundManager.Instance.PlayFootstepsSound(_pawn.transform.position, _volume);
            }
        }
    }
}