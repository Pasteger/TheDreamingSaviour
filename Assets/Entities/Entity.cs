using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp;
    public int damage;
    private int _maxHp;
    
    public List<AudioClip> damageSounds;
    public List<AudioClip> attackSounds;
    public AudioClip deathSound;
    
    public GameObject deathBehaviour;
    
    public readonly SoundBehaviour SoundBehaviour = new ();

    private void Start()
    {
        SoundBehaviour.SetAudioSource(GetComponent<AudioSource>());
    }
    
    private void Update()
    {
        if (hp < 1)
        {
            Death();
        }
    }

    public void Hit(int hitDamage)
    {
        SoundBehaviour.PlaySound(damageSounds);
        hp -= hitDamage;
    }

    private void Death()
    {
        var localDeathBehaviour = Instantiate(deathBehaviour, transform.position, Quaternion.identity);
        var entityDeathBehaviour = localDeathBehaviour.GetComponent<EntityDeathBehaviour>();
        entityDeathBehaviour.Initialize(GetComponent<Renderer>().material.mainTexture, deathSound);
        
        Destroy(gameObject);
    }
}
