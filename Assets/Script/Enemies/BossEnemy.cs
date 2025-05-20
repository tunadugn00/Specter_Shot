using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedBullet;
    [SerializeField] private float healValue = 50f;
    [SerializeField] GameObject miniEnemy;
    [SerializeField] private float skillCooldown = 2f;
    private float nextSkillTime = 0f;
    [SerializeField] private GameObject chestPrefabs;
    
    
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

    protected override void Update()
    {
        base.Update();
        if (isPlayerInRange && player != null && Time.time >= lastDamageTime + damageDelay)
        {
            player.TakeDamage(stayDMG, transform.position);
            lastDamageTime = Time.time;
        }

        if(Time.time >= nextSkillTime)
        {
            UseSkill();
        }
    }
    public override void TakeDamage(float damage, Vector2 attackerPos)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
        UpdateHpBar();

        // no knockback
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Instantiate(chestPrefabs, transform.position, Quaternion.identity);
        base.Die();
    }


    //SKill Boss
    private void NormalAttack()
    {
        if(player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedBullet);
        }
    }
    private void CircleShot()
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletDirection * speedBullet);
        }
    }
    private void HealBoss(float hpAmount)
    {
        currentHP = Mathf.Min(currentHP + hpAmount, maxHP);
        UpdateHpBar();
    }
    private void SpawnMiniEnemy()
    {
        Instantiate(miniEnemy, transform.position, Quaternion.identity);
    }
    private void ChargeAttack()
    {

    }

    private void RandomSkill()
    {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill)
        {
            case 0:
                NormalAttack(); break;
            case 1:
                CircleShot(); break;
            case 2:
                HealBoss(healValue); break;
            case 3:
                SpawnMiniEnemy(); break;
        }
    }
    private void UseSkill()
    {
        nextSkillTime = Time.time + skillCooldown;
        RandomSkill();
    }
}
