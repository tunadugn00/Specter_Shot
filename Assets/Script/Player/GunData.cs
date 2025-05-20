using UnityEngine;


[CreateAssetMenu(menuName = "Data/Gun")]
public class GunData : ScriptableObject
{
    public string gunName;
    public Sprite icon;

    public float baseDamage;
    public float fireRate;
    public float projectileSpeed;
    public float attackRange;

}
