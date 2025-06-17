using UnityEngine;


[CreateAssetMenu(menuName = "Data/Gun")]
public class GunData : ScriptableObject
{
    public string id;
    public int price;
    public string gunName;
    public Sprite icon;
    public GameObject prefab;

    public float baseDamage;
    public float fireRate;
    public float projectileSpeed;
    public float attackRange;

}
