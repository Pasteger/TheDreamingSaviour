using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    public int hp;
    public int damage;
    
    public List<DropItem> drops;
    
    public List<AudioClip> damageSounds;
    public List<AudioClip> attackSounds;
    public AudioClip deathSound;
    
    public GameObject deathBehaviour;
    
    public readonly SoundBehaviour SoundBehaviour = new ();

    private int _hp;
    
    private void Start()
    {
        SoundBehaviour.SetAudioSource(GetComponent<AudioSource>());

        _hp = hp;
    }
    
    private void Update()
    {
        if (_hp < 1)
        {
            Death();
        }
    }

    public void Hit(int hitDamage)
    {
        SoundBehaviour.PlaySound(damageSounds);
        _hp -= hitDamage;
    }

    private void Death()
    {
        var localDeathBehaviour = Instantiate(deathBehaviour, transform.position, Quaternion.identity);
        var entityDeathBehaviour = localDeathBehaviour.GetComponent<EntityDeathBehaviour>();
        entityDeathBehaviour.Initialize(ExtractTexture(), deathSound);

        if (drops.Count != 0)
        {
            DropItems();
        }

        Destroy(gameObject);
    }

    private void DropItems()
    {
        foreach (var item in drops)
        {
            if (Random.Range(0f, 1f) > item.chance) continue;
            
            var position = transform.position;
            var x = position.x + Random.Range(-0.5f, 0.5f);
            var y = position.y + Random.Range(-0.5f, 0.5f);

            var itemPosition = new Vector3(x, y, position.z);
            
            Instantiate(item.item, itemPosition, Quaternion.identity);
        }
    }
    
    private Texture ExtractTexture()
    {
        Texture texture = new Texture2D(10, 10);

        var componentRenderer = GetComponent<Renderer>();
        if (componentRenderer != null)
        {
            return componentRenderer.material.mainTexture;
        }
        
        var childrenRenderer = transform.GetComponentInChildren<Renderer>();
        if (childrenRenderer != null)
        {
            return childrenRenderer.material.mainTexture;
        }

        return texture;
    }
}
