using UnityEngine;

public class EntityDeathBehaviour : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private ParticleSystemRenderer _particleSystemRenderer;
    private AudioSource _audioSource;

    private Texture _particleTexture;
    private AudioClip _deathSound;
    
    
    public void Initialize(Texture particleTexture, AudioClip deathSound)
    {
        _particleTexture = particleTexture;
        _deathSound = deathSound;
    }
    
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        _audioSource = GetComponent<AudioSource>();
        
        _particleSystemRenderer.material.mainTexture = _particleTexture;
        
        var textureSheetAnimation = _particleSystem.textureSheetAnimation;
        textureSheetAnimation.enabled = true;
        textureSheetAnimation.mode = ParticleSystemAnimationMode.Sprites;
        textureSheetAnimation.numTilesX = 4;
        textureSheetAnimation.numTilesY = 4;
        textureSheetAnimation.frameOverTime = new ParticleSystem.MinMaxCurve(0, 1);
        
        _audioSource.clip = _deathSound;
        
        _particleSystem.Play();
        _audioSource.Play();
    }
    
    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
