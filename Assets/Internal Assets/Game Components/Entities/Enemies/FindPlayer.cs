using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    public GameObject player;

    private Entity _thisEntity;
    private Entity _playerEntity;
    
    private EntityMovement _entityMovement;

    private int _hitDelay;
    
    private void Awake()
    {
        _entityMovement = GetComponent<EntityMovement>();
        _thisEntity = GetComponent<Entity>();
        _playerEntity = player.GetComponent<Entity>();
    }
    
    private void FixedUpdate()
    {
        if (_hitDelay != 0)
        {
            _hitDelay--;
        }

        if (!(player.transform.position.x - transform.position.x < 10)) return;
        
        var x = player.transform.position.x - transform.position.x;
        var y = player.transform.position.y - transform.position.y;

        var horizontalInput = x > 0 ? 1 : -1;
        var verticalInput = y > 0 ? 1 : -1;
            
        _entityMovement.Move(horizontalInput, verticalInput);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.Equals(player) || _hitDelay != 0) return;

        _thisEntity.SoundBehaviour.PlaySound(_thisEntity.attackSounds);
        _playerEntity.Hit(_thisEntity.damage);
        _hitDelay = 20;
    }
}
