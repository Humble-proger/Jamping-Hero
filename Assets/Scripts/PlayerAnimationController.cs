using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMover _playerMover;

    private readonly int Run = Animator.StringToHash("Run");
    private readonly int Jump = Animator.StringToHash("Jump");
    private readonly int Fall = Animator.StringToHash("Fall");

    private void OnEnable()
    {
        _playerMover.GroundStateChenged += SetFall;
        _playerMover.RunStateChenged += SetRun;
        _playerMover.Jump += SetJump;
    }
    private void OnDisable()
    {
        _playerMover.GroundStateChenged -= SetFall;
        _playerMover.RunStateChenged -= SetRun;
        _playerMover.Jump -= SetJump;
    }

    public void SetRun(bool mode) {
        _animator.SetBool(Run, mode);
    }
    
    public void SetFall(bool mode)
    {
        _animator.SetBool(Fall, !mode);
    }
    public void SetJump()
    {
        _animator.SetTrigger(Jump);
    }
}