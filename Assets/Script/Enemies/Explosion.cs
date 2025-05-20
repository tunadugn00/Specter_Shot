using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float DMG = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();

        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(DMG, transform.position);
        }
        if (collision.CompareTag("Enemy"))
        {
            enemy.TakeDamage(DMG, transform.position);
        }
    }

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
