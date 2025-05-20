using UnityEngine;

public class Energy : MonoBehaviour
{
    public int energyValue = 2;
    private float magnetSpeed = 7f;

    private Transform player;
    private PlayerStats playerStats;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null)
        {
            player = playerObj.transform;
            playerStats = playerObj.GetComponent<PlayerStats>();
        }
    }

    private void Update()
    {
        if (player == null || playerStats == null) return;

        float radius = playerStats.energyMagnetRadius;
        if (radius <= 0) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if(distance <= radius )
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * magnetSpeed * Time.deltaTime;    
        }
    }
}
