using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    public CoinValues value = CoinValues.One;

    public AudioClip pickSound;

    private void Update()
    {
        transform.Rotate(0, 0, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        other.gameObject.GetComponent<PlayerController>().Balance += (int) value;
        
        AudioSource.PlayClipAtPoint(pickSound, transform.position, 1f);
        
        Destroy(gameObject);
    }
}
