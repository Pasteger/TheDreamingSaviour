using UnityEngine;

public class EntityRotations : MonoBehaviour
{
    public GameMode gameMode = GameMode.Platformer;

    private Vector3 _directionView = Vector3.right;

    public Vector3 localDown = Vector3.down;

    private Vector3 _localDown = Vector3.down;

    public Vector3 DefineDirectionView()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            _directionView = Vector3.right;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            _directionView = Vector3.left;
        }

        if (gameMode != GameMode.Platformer)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                _directionView = Vector3.up;
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                _directionView = Vector3.down;
            }
        }

        return _directionView;
    }

    public void ChangeGameMode()
    {
        if (_localDown == localDown) return;

        switch (gameMode)
        {
            case GameMode.Platformer:
                if (transform.rotation is not { y: 0, z: 0 })
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                break;
            case GameMode.Tanks:
                if (transform.rotation is not { x: 270 })
                {
                    transform.rotation = Quaternion.Euler(270, 0, 0);
                }
                break;
        }

        _localDown = localDown;
    }

    public void Rotate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (_localDown == Vector3.down) RotateToPlatformerDown(horizontalInput);
        if (_localDown == Vector3.forward) RotateToTanksDown(horizontalInput, verticalInput);
        //TODO Сделать методы для остальных локальных низов
    }

    private void RotateToPlatformerDown(float horizontalInput)
    {
        var rightRotation = new Vector3(0, 90, 0);
        var leftRotation = new Vector3(0, 270, 0);

        var rotation = transform.rotation;
        
        rotation = horizontalInput switch
        {
            > 0 when rotation != Quaternion.Euler(rightRotation) => Quaternion.Euler(rightRotation),
            < 0 when rotation != Quaternion.Euler(leftRotation) => Quaternion.Euler(leftRotation),
            _ => rotation
        };
        
        transform.rotation = rotation;
    }

    private void RotateToTanksDown(float horizontalInput, float verticalInput)
    {
        var rightRotation = new Vector3(180, 270, 90);
        var leftRotation = new Vector3(0, 270, 90);
        var upRotation = new Vector3(270, 270, 90);
        var downRotation = new Vector3(90, 270, 90);

        var rotation = transform.rotation;
        
        rotation = horizontalInput switch
        {
            > 0 when rotation != Quaternion.Euler(rightRotation) => Quaternion.Euler(rightRotation),
            < 0 when rotation != Quaternion.Euler(leftRotation) => Quaternion.Euler(leftRotation),
            _ => rotation
        };

        rotation = verticalInput switch
        {
            > 0 when rotation != Quaternion.Euler(upRotation) => Quaternion.Euler(upRotation),
            < 0 when rotation != Quaternion.Euler(downRotation) => Quaternion.Euler(downRotation),
            _ => rotation
        };
        
        transform.rotation = rotation;
    }
}
