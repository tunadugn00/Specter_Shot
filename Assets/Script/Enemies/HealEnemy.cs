using UnityEngine;

public class HealEnemy : Enemy
{
    [SerializeField] private float healValue = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player != null)
        {
            player.TakeDamage(enterDMG, transform.position);
            lastDamageTime = Time.time;
            isPlayerInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    protected override void Die()
    {
        HealPlayer();
        base.Die();
    }

    private void HealPlayer()
    {
        if(player != null)
        {
            player.Heal(healValue);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isPlayerInRange && player != null && Time.time >= lastDamageTime + damageDelay)
        {
            player.TakeDamage(stayDMG, transform.position);
            lastDamageTime = Time.time;
        }
    }
}
