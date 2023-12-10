using UnityEngine;

public class PawnAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string ON_ROLL_JUMP = "OnRollJump";

    [SerializeField] private Pawn _pawn;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _pawn.OnJump += PlayJumpAnimation;
    }

    private void PlayJumpAnimation()
    {
        _animator.SetTrigger(ON_ROLL_JUMP);
    }

    private void Update()
    {
        _animator.SetBool(IS_WALKING, _pawn.IsWalking());
    }
}