using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private PlayerStats stats;
    public PlayerStats Stats
    {
        get { return stats; }
    }


    [SerializeField] Image hpBar;
    private float regenTimer;
    private float damageCooldown = 0.5f; 
    private float lastDamageTime = -999f;
    private bool isInvincible = false;
    private int flashCount = 3;

    public JoyStickMovement joyStickMovement;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public AudioClip playerHitClip;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        stats.currentHP = stats.maxHP;
        stats.player = this;
        UpdateHpBar();
    }
    private void Update()
    {
        RegenHP();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 moveDirection = new Vector2(joyStickMovement.joystickVec.x, 
                                            joyStickMovement.joystickVec.y);

        if (moveDirection != Vector2.zero)
        {
            rb.linearVelocity = moveDirection * stats.moveSpeed;

            spriteRenderer.flipX = moveDirection.x < 0;

            animator.SetBool("isRun", true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isRun", false); 
        }
    }


    public void TakeDamage(float damage, Vector2 attackerPos)
    {
        if (isInvincible | Time.time - lastDamageTime < damageCooldown) return;

        float armor = stats.armor;
        float damageReduction = 100f/(100f + armor);
        float finalDamage = damage * damageReduction;
        

        stats.currentHP -= finalDamage;
        stats.currentHP = Mathf.Max(stats.currentHP, 0);
        UpdateHpBar();

        lastDamageTime = Time.time;

        if (stats.currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
        AudioManager.Instance.PlaySFX(playerHitClip);


    }
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        for(int i = 0; i< flashCount; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(damageCooldown / (flashCount * 2));

            spriteRenderer.color = Color.white; 
            yield return new WaitForSeconds(damageCooldown / (flashCount * 2));
        }
        isInvincible = false;
    }

    private void Die()
    {
        GameManager.Instance.SetState(GameState.GameOver);
    }

    public void Heal (float healValue)
    {
        if(stats.currentHP < stats.maxHP)
        {
            stats.currentHP += healValue;
            stats.currentHP = Mathf.Min(stats.currentHP, stats.maxHP);
            UpdateHpBar();
        }
    }
    public void HealNextWave()
    {
        stats.currentHP = stats.maxHP;
        UpdateHpBar() ;
    }
    public void RegenHP()
    {
        if(stats.currentHP < stats.maxHP)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer > 3f)
            {
                stats.currentHP += stats.regenHP * Time.deltaTime;
                stats.currentHP = Mathf.Min(stats.currentHP, stats.maxHP);
                UpdateHpBar();
                regenTimer = 0f;
            }
        }
    }
    private void UpdateHpBar()
    {
        if(hpBar != null)
        {
            hpBar.fillAmount = stats.currentHP / stats.maxHP;
        }
    }



}
