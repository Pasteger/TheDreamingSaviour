using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public GravityDirections direction = GravityDirections.Down;
    public GameMode gameMode = GameMode.Platformer;

    private readonly Dictionary<int, Rigidbody> _rigidbodies = new();

    private Vector3 _direction;

    private Renderer _renderer;

    private void Start()
    {
        Physics.gravity = Vector3.zero;
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case GravityDirections.Up:
                _direction = new Vector3(0, 9.81f, 0);
                _renderer.material.color = new Color(0.52f, 0, 0.71f, 0.1f);
                break;
            case GravityDirections.Down:
                _direction = new Vector3(0, -9.81f, 0);
                _renderer.material.color = new Color(0.31f, 0.17f, 0.83f, 0.1f);
                break;
            case GravityDirections.Right:
                _direction = new Vector3(9.81f, 0, 0);
                _renderer.material.color = new Color(0.71f, 0, 0, 0.1f);
                break;
            case GravityDirections.Left:
                _direction = new Vector3(-9.81f, 0, 0);
                _renderer.material.color = new Color(0.43f, 0.69f, 0.67f, 0.1f);
                break;
            case GravityDirections.Forward:
                _direction = new Vector3(0, 0, 9.81f);
                _renderer.material.color = new Color(0.83f, 0.76f, 0.17f, 0.1f);
                break;
            case GravityDirections.Back:
                _direction = new Vector3(0, 0, -9.81f);
                _renderer.material.color = new Color(0.19f, 0.75f, 0.13f, 0.1f);
                break;
            default:
                _direction = new Vector3(0, -9.81f, 0);
                _renderer.material.color = new Color(1, 1, 1, 0.1f);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherHashCode = other.gameObject.GetHashCode();

        if (_rigidbodies.ContainsKey(otherHashCode)) return;

        var otherRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if (otherRigidbody == null || !otherRigidbody.useGravity) return;
        
        _rigidbodies.Add(otherHashCode, otherRigidbody);
    }

    private void OnTriggerStay(Collider other)
    {
        var otherHashCode = other.gameObject.GetHashCode();

        if (!_rigidbodies.ContainsKey(otherHashCode)) return;

        var otherPlayerRotation = other.gameObject.GetComponent<EntityRotations>();
        if (otherPlayerRotation != null)
        {
            otherPlayerRotation.gameMode = gameMode;
            otherPlayerRotation.localDown = _direction / 9.81f;
        }
        
        var rb = _rigidbodies[otherHashCode];

        var forceVector = _direction * rb.mass;

        rb.AddForce(forceVector, ForceMode.Force);
    }

    private void OnTriggerExit(Collider other)
    {
        var otherHashCode = other.gameObject.GetHashCode();

        if (!_rigidbodies.ContainsKey(otherHashCode)) return;

        var rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null) _rigidbodies.Remove(otherHashCode);
    }
}
