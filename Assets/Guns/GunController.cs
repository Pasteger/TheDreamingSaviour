using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public int shotDelay;
    public int damage;
    
    public List<AudioClip> shotSound;
    private readonly SoundBehaviour _soundBehaviour = new ();
    
    private int _shotDelay;
    
    private void Start()
    {
        _soundBehaviour.SetAudioSource(GetComponent<AudioSource>());
    }

    public void Shot(Vector3 directionView, Collider sender)
    {
        if (_shotDelay == 0)
        {
            var offsetX = transform.localScale.x * directionView.x;
            var offsetY = transform.localScale.y * directionView.y;
            var offset = new Vector3(offsetX, offsetY, 0);
                
            var positionBullet = transform.position + offset;
                
            var bull = Instantiate(bullet, positionBullet, Quaternion.identity);
                
            bull.GetComponent<BulletLogic>().Initialize(
                directionView, bulletSpeed, damage, sender);
            
            _soundBehaviour.PlaySound(shotSound);
                
            _shotDelay = shotDelay;
        }
        else
        {
            _shotDelay--;
        }
    }
}
