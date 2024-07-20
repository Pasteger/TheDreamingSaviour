using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Balance;
    
    private EntityMovement _entityMovement;

    private void Awake()
    {
        _entityMovement = GetComponent<EntityMovement>();
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        _entityMovement.Move(horizontalInput, verticalInput);
    }
}
