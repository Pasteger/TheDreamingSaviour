using System;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private float _maxSpeed;
    private Vector3 _direction;
    private Collider _sender;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 direction, float speed, int damage, Collider sender)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _sender = sender;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Math.Abs(_rigidbody.velocity.magnitude) < _speed)
        {
            _rigidbody.AddForce(_speed * _direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || other.gameObject.CompareTag("Bullet") || other.Equals(_sender))
        {
            return;
        }

        var entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            entity.Hit(_damage);
        }
        
        Destroy(gameObject);
    }
}
