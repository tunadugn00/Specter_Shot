using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerStats playerStats;
    public AudioClip pickupClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chest"))
        {
            Debug.Log("VICTORY");
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Energy"))
        {
            Energy energy = collision.GetComponent<Energy>();
            if (energy != null)
            {
                playerStats.AddEnergy(energy.energyValue);
                AudioClip clip = pickupClip;
                Destroy(collision.gameObject) ;
                AudioManager.Instance.PlaySFX(pickupClip);
            }
        }
    }
}
