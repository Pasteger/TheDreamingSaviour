using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedWalk;
    public float maxSpeedWalk;
    public float forceJump;
    
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    private int _jumpDelay;

    private Animator _animator;

    private static readonly int RunKeyDown = Animator.StringToHash("RunKeyDown");

    private PlayerRotations _playerRotations;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        _playerRotations = GetComponent<PlayerRotations>();
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    private void MoveLogic()
    {
        _playerRotations.ChangeGameMode();
        _playerRotations.Rotate();

        switch (_playerRotations.gameMode)
        {
            case GameMode.Platformer:
                MoveGravity();
                JumpLogic();
                break;
            case GameMode.Tanks:
                MoveUnGravity();
                break;
        }
    }

    private void MoveGravity()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        _animator.SetBool(RunKeyDown, horizontalInput != 0);

        var direction = new Vector3(horizontalInput, 0, 0);

        if (Math.Abs(_rigidbody.velocity.x) < maxSpeedWalk)
        {
            _rigidbody.AddForce(direction * speedWalk, ForceMode.Acceleration);
        }
    }

    private void MoveUnGravity()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        _animator.SetBool(RunKeyDown, horizontalInput != 0 || verticalInput != 0);

        if (Math.Abs(_rigidbody.velocity.x) > maxSpeedWalk)
        {
            horizontalInput = 0;
        }

        if (Math.Abs(_rigidbody.velocity.y) > maxSpeedWalk)
        {
            verticalInput = 0;
        }

        var direction = new Vector3(horizontalInput, verticalInput, 0);

        _rigidbody.AddForce(direction * speedWalk, ForceMode.Acceleration);
    }

    private void JumpLogic()
    {
        if (_jumpDelay > 0)
        {
            _jumpDelay--;
        }

        if (IsGrounded())
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (_jumpDelay == 0)
                {
                    _rigidbody.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
                    _jumpDelay = 10;
                }
            }
        }
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, _collider.height / 1.9f))
        {
            return hit.collider != null;
        }

        return false;
    }
}