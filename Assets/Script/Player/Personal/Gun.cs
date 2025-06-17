using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private ProjectTile projectTilePrefab;
    [SerializeField] private Transform firePoint;

    private float projectileSpeed;
    private float attackRange; 
    private float nextFireTime;
    private Enemy[] enemies;

    private void Start()
    {
        if (gunData != null)
        {
            projectileSpeed = gunData.projectileSpeed;
            attackRange = gunData.attackRange;

            if (PlayerStats.Instance != null)
            {
                PlayerStats.Instance.SetGunData(gunData);
            }
        }
        else
        {
            Debug.LogWarning("GunData not assigned on Gun!");
        }
    }

    private void Update()
    {
        ShootNearestEnemy();
    }

    void ShootNearestEnemy()
    {
        enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        if (enemies.Length == 0) return;

        Enemy nearestEnemy = GetNearestEnemy();
        if (nearestEnemy == null) return;

        AimGun(nearestEnemy);

        if (Time.time >= nextFireTime)
        {
            FireProjectile(nearestEnemy);
            nextFireTime = Time.time + (1f / PlayerStats.Instance.fireRate);
        }
    }

    Enemy GetNearestEnemy()
    {
        Enemy nearest = null;
        float minDistance = Mathf.Infinity;
        Vector2 gunPosition = transform.position;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector2.Distance(gunPosition, enemy.transform.position);
            if (distance < minDistance && distance <= attackRange)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void AimGun(Enemy target)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.localScale = new Vector3(1, angle > 90f || angle < -90f ? -1 : 1, 1);
    }

    void FireProjectile(Enemy target)
    {
        ProjectTile projectile = Instantiate(projectTilePrefab, firePoint.position, Quaternion.identity);
        projectile.SetDamage(PlayerStats.Instance.damage);

        Vector2 direction = (target.transform.position - firePoint.position).normalized;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
        if (firePoint == null || target == null)
        {
            Debug.LogWarning("Missing firePoint or target");
            return;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
