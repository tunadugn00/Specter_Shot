using UnityEngine;

public class BasicEnemy : Enemy
{    
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
            isPlayerInRange = true ;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
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
