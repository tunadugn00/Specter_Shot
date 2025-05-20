using UnityEngine;

public enum ItemTier { D, C, B, A, S}

[CreateAssetMenu(fileName = "UpgradeItem", menuName = "Shop/Upgrade Item")]
[System.Serializable]
public class UpgradeItem : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public string description;
    public int price;

    public string statToUpgrade;
    public float upgradeAmount;

    public ItemTier itemTier;
}
