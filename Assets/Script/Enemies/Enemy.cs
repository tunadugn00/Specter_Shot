using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected float enemySpeed = 1f;
    [SerializeField] protected float maxHP = 20f;
    [SerializeField] protected float enterDMG = 1f;
    [SerializeField] protected float stayDMG = 2f;
    [SerializeField] protected float damageDelay = 0.5f;
    protected float lastKnockbackTime = -999f;
    [SerializeField] private float knockbackCooldown = 2f;
    protected bool isKnockedBack = false;
    [SerializeField] private float knockbackDuration = 0.2f;

    protected float currentHP;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject energyObject;

    protected Player player;
    protected float lastDamageTime;
    protected bool isPlayerInRange;

    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        currentHP = maxHP;
        UpdateHpBar();
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            MoveToPlayer();
        }
        
    }

    protected void MoveToPlayer()
    {
        if (isKnockedBack) return;
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                                  player.transform.position, enemySpeed * Time.deltaTime);
            FlipEnemy();
        }
    }

    protected void FlipEnemy()
    {
        float scaleX = player.transform.position.x < transform.position.x ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    public virtual void TakeDamage(float damage, Vector2 attackerPos)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
        UpdateHpBar();

        //Knockback
        if ( Time.time - lastKnockbackTime >= knockbackCooldown)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockDir = (transform.position - (Vector3)attackerPos).normalized;
                if (knockDir == Vector2.zero) knockDir = Vector2.up;

                float knockbackForce = PlayerStats.Instance != null ? PlayerStats.Instance.knockbackForce : 5f;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
                
                isKnockedBack = true;
                Invoke(nameof(RecoverFromKncokback), knockbackDuration);
                lastKnockbackTime = Time.time;
            }
        }


        if (currentHP <= 0)
        {
            Die();
        }
    }
    protected virtual void RecoverFromKncokback()
    {
        isKnockedBack = false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        if (energyObject != null)
        {
            GameObject enegy = Instantiate(energyObject, transform.position, Quaternion.identity);
            
        }
    }

    protected void UpdateHpBar()
    {
        if(hpBar != null)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }
}

