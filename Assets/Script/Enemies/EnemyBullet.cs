using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float DMGBullet = 3f;
    private Vector3 movementDirection;

    void Start()
    {
        Destroy(gameObject,5f);
    }

    
    void Update()
    {
        if (movementDirection == Vector3.zero) return;
        transform.position += movementDirection * Time.deltaTime;
    }

    public void SetMovementDirection (Vector3 direction)
    {
        movementDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(DMGBullet, transform.position);
            }
            
        }
    }
}
