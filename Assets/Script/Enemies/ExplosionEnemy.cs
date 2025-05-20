using UnityEngine;

public class ExplosionEnemy : Enemy
{
    [SerializeField] private GameObject explosionPrefab;

    private void CreateExplosion()
    {
        if(explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CreateExplosion();
        }
    }
}
