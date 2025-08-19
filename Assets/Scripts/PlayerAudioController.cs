using UnityEngine;

public class PlayerAudioController : MonoBehaviour {
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _landedClip;
    [SerializeField] private AudioClip _runingClip;

    private void OnEnable()
    {
        _mover.Jump += PlayJump;
        _mover.RunStateChenged += PlayRun;
        _mover.GroundStateChenged += PlayLand;
    }
    private void OnDisable()
    {
        _mover.Jump -= PlayJump;
        _mover.RunStateChenged -= PlayRun;
        _mover.GroundStateChenged -= PlayLand;
    }
    private void Start()
    {
        _playerAudioSource.clip = _runingClip;
    }
    private void PlayLand(bool obj)
    {
        if (obj)
            AudioSource.PlayClipAtPoint(_landedClip, transform.position);
    }

    private void PlayRun(bool obj)
    {
        if (obj)
            _playerAudioSource.Play();
        else
            _playerAudioSource.Stop();
    }

    private void PlayJump()
    {
        AudioSource.PlayClipAtPoint(_jumpClip, transform.position);
    }
}