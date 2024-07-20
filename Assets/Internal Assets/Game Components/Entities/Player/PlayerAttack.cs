using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject gun;

    private EntityRotations _playerRotations;
    private GunController _gunController;

    private Collider _collider;
    
    private Vector3 _directionView = Vector3.right;
    private int _shotDelay;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _playerRotations = GetComponent<EntityRotations>();
        _gunController = gun.GetComponent<GunController>();
    }

    private void FixedUpdate()
    {
        _directionView = _playerRotations.DefineDirectionView();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _gunController.Shot(_directionView, _collider);
        }
    }
}
