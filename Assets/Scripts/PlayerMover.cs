using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jampForce;
    [SerializeField] private float _rayCastHeight; //Длина нуча должна быть чуть больше половины длины персонажа 
    [SerializeField] private float _rayCastWidth;
    [SerializeField] private Transform _pointDownRaycast;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rb;
    private PlayerController _playerController;
    private Vector2 _horizontal;
    private bool _isGround = true;
    private bool _isRunning = false;
    private Side _wallSide = 0;
    private enum Side {
        None,
        Right,
        Left
    }

    public event Action<bool> GroundStateChenged;
    public event Action<bool> RunStateChenged;
    public event Action Jump;

    private void OnEnable()
    {
        _playerController.Player.Jump.performed += OnJump;
        _playerController.Player.Enable();
    }
    private void OnDisable()
    {
        _playerController.Player.Disable();
        _playerController.Player.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isGround)
        {
            Jump?.Invoke();
            _rb.linearVelocityY = 0f;
            _rb.AddForce(Vector2.up * _jampForce, ForceMode2D.Impulse);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerController = new PlayerController();
    }

    private bool IsGround() {
        RaycastHit2D ray = Physics2D.Raycast(_pointDownRaycast.position, Vector2.down, _rayCastHeight, _layerMask);
        return ray.collider != null;
    }

    private void Update()
    {
        CheckWallCollision();
        bool ground = IsGround();
        if (ground != _isGround)
            GroundStateChenged?.Invoke(ground);
        _isGround = ground;

        _horizontal = _playerController.Player.Move.ReadValue<Vector2>();
        switch (_wallSide) {
            case Side.Right:
                _horizontal.x = Mathf.Clamp(_horizontal.x, -1f, 0f);
                break;
            case Side.Left:
                _horizontal.x = Mathf.Clamp01(_horizontal.x);
                break;
        }
        CheckRunState(_horizontal.x);
        if (_horizontal.x > 0)
            _spriteRenderer.flipX = false;
        if (_horizontal.x < 0)
            _spriteRenderer.flipX = true;
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_horizontal.x * _speed, _rb.linearVelocity.y);
    }

    private void CheckWallCollision()
    {
        bool hitTopRight = Physics2D.Raycast(transform.position, Vector2.right, _rayCastWidth, _layerMask);
        bool hitTopLeft = Physics2D.Raycast(transform.position, Vector2.left, _rayCastWidth, _layerMask);
        bool hitBottomRight = Physics2D.Raycast(_pointDownRaycast.position, Vector2.right, _rayCastWidth, _layerMask);
        bool hitBottomLeft = Physics2D.Raycast(_pointDownRaycast.position, Vector2.left, _rayCastWidth, _layerMask);

        if (hitTopRight || hitBottomRight)
            _wallSide = Side.Right;
        else if (hitTopLeft || hitBottomLeft)
            _wallSide = Side.Left;
        else _wallSide = Side.None;
    }

    private void CheckRunState(float moveX) {
        bool trigger;
        if (moveX.Equals(0f))
            trigger = false;
        else
            trigger = true;
        if (trigger != _isRunning)
        {
            RunStateChenged?.Invoke(trigger);
            _isRunning = trigger;
        }
    }
}