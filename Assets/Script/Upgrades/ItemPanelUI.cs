using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text desText;
    public Button buyButton;
    public TMP_Text priceText;

    private UpgradeItem itemData;

    public void SetItem(UpgradeItem item)
    {
        itemData = item;

        iconImage.sprite = item.icon;
        nameText.text = item.itemName;
        desText.text = item.description;
        priceText.text = "-" + item.price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyItem);

    }

    void BuyItem()
    {
        var stats = PlayerStats.Instance;

        if(stats.energy >= itemData.price)
        {
            stats.AddEnergy(-itemData.price);
            stats.ApplyUpgrade(itemData.statToUpgrade, itemData.upgradeAmount);
            buyButton.interactable = false;
        }
        else
        {
            Debug.Log("Not enough energy");
        }
    }
}
