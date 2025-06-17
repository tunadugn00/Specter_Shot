using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    public string id;
    public int price;
    public string characterName;
    public Sprite icon;
    public GameObject prefab;

    public float baseHP;
    public float baseSpeed;
    public float armor;
    public float regenHP;

    public float critRate;
    public float critDamage;
    public float lifeSteal;
}
