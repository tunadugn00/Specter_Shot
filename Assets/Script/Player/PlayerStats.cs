using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [SerializeField] private CharacterData characterData;
    [SerializeField] private GunData gunData;
    [SerializeField] private TMP_Text energyText;
    public Player player;


    [Header("Stats")]
    public float maxHP;
    public float currentHP;
    public float regenHP;
    public float armor;
    public float moveSpeed;
    public float knockbackForce;
    public float critRate;
    public float critDamage;
    public float lifeSteal;
    public float energyMagnetRadius = 3f;
    public int energy = 0;

    [Header("Weapon Stats")]
    public float damage;
    public float fireRate;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        InitCharacter(characterData, gunData);
    }

    private void Start()
    {
        
        UpdateEnergyUI();
    }

    public void InitCharacter(CharacterData charData, GunData gunData)
    {
        if (charData == null) return;

        characterData = charData;

        maxHP = charData.baseHP;
        currentHP = maxHP;
        moveSpeed = charData.baseSpeed;
        armor = charData.armor;
        regenHP = charData.regenHP;

        critRate = charData.critRate;
        critDamage = charData.critDamage;
        lifeSteal = charData.lifeSteal;
    }

    public void SetGunData(GunData gunData)
    {
        if (gunData == null)
        {
            Debug.LogWarning("GunData is missing!");
            return;
        }

        damage = gunData.baseDamage;
        fireRate = gunData.fireRate;
    }

    public void AddEnergy(int amount)
    {
        energy += amount;
        UpdateEnergyUI();
    }

    private void UpdateEnergyUI()
    {
        if (energyText != null)
        {
            energyText.text = energy.ToString();
        }
    }

    public void ApplyUpgrade(string stat, float amount)
    {
        switch (stat.ToLower())
        {
            case "damage": damage += amount; break;
            case "maxhp":
                maxHP += amount;
                currentHP = Mathf.Min(currentHP + amount, maxHP);
                break;
            case "firerate": fireRate += amount; break;
            case "movespeed": moveSpeed += amount; break;
            case "armor": armor += amount; break;
            case "lifesteal": lifeSteal += amount; break;
            case "regenhp": regenHP += amount; break;
            case "critrate": critRate += amount; break;
            case "critdamage": critDamage += amount; break;
            case "knockback": knockbackForce += amount; break;
            case "magnetradius": energyMagnetRadius += amount; break;
            default:
                Debug.LogWarning("Unknown stat upgrade: " + stat);
                break;
        }
    }
}
