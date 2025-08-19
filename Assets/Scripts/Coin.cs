using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour, ICollectible
{
    [SerializeField] private AudioClip _coinCollect;
    public void Collect()
    {
        AudioSource.PlayClipAtPoint(_coinCollect, transform.position);
        Destroy(transform.parent.gameObject);
    }

    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ScoreCounter player))
        {
            player.AddScore(1);
            Collect();
        }
    }
}