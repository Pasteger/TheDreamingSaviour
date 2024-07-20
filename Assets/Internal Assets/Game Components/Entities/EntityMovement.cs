using System;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speedWalk;
    public float maxSpeedWalk;
    public float forceJump;
    
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    private int _jumpDelay;

    private Animator _animator;

    private static readonly int Run = Animator.StringToHash("Run");

    private EntityRotations _playerRotations;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        _playerRotations = GetComponent<EntityRotations>();
    }
    
    public void Move(float horizontalInput, float verticalInput)
    {
        _playerRotations.ChangeGameMode();
        _playerRotations.Rotate();

        switch (_playerRotations.gameMode)
        {
            case GameMode.Platformer:
                MovePlatformer(horizontalInput);
                JumpLogic(verticalInput);
                break;
            case GameMode.Tanks:
                MoveTanks(horizontalInput, verticalInput);
                break;
        }
    }

    private void MovePlatformer(float horizontalInput)
    {
        if (_animator != null)
        {
            _animator.SetBool(Run, horizontalInput != 0);
        }

        var direction = new Vector3(horizontalInput, 0, 0);

        if (Math.Abs(_rigidbody.velocity.x) < maxSpeedWalk)
        {
            _rigidbody.AddForce(direction * speedWalk, ForceMode.Acceleration);
        }
    }

    private void MoveTanks(float horizontalInput, float verticalInput)
    {
        if (_animator != null)
        {
            _animator.SetBool(Run, horizontalInput != 0 || verticalInput != 0);
        }

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

    private void JumpLogic(float verticalInput)
    {
        if (_jumpDelay > 0)
        {
            _jumpDelay--;
        }

        if (IsGrounded())
        {
            if (verticalInput > 0)
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
