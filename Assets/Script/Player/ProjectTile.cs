using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    private float basedamage;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private GameObject bloodPrefab;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        if (rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    public void SetDamage(float dmg)
    {
        basedamage = dmg;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                float damagetoDeal = basedamage;

                // Crit
                float critChance = PlayerStats.Instance.critRate;
                float critMultiplier = PlayerStats.Instance.critDamage;
                bool isCrit = Random.value <= critChance;

                if (isCrit)
                {
                    damagetoDeal *=critMultiplier;
                    Debug.Log("Critical" + damagetoDeal);
                }
                enemy.TakeDamage(damagetoDeal, transform.position);

                // Lifesteal
                float lifesteal = PlayerStats.Instance.lifeSteal;
                float healAmount = damagetoDeal * lifesteal;
                if(healAmount > 0)
                {
                    PlayerStats.Instance.player.Heal(healAmount);
                    Debug.Log("Lifesteal" + healAmount);
                }


                GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
                Destroy(blood, 1f);
            }
            Destroy(gameObject);
        }
    }
}
