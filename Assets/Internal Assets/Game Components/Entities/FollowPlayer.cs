using UnityEngine;

//Временный заменитель искуственного интеллекта
public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public bool lockX;
    public bool lockY;
    public bool lockZ;

    private void LateUpdate()
    {
        var playerPosition  = player.transform.position;
        
        var x = lockX ? transform.position.x : playerPosition.x ;
        var y = lockY ? transform.position.y : playerPosition.y;
        var z = lockZ ? transform.position.z : playerPosition.z;
        
        transform.position = new Vector3(x, y, z);;
    }
}
