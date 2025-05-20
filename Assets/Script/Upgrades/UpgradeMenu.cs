using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public static UpgradeMenu Instance;

    public GameObject panel;
    public GameObject itemPanel;
    public Transform contentHolder;
    public List<UpgradeItem> allItems = new List<UpgradeItem>();

    public int rollCost = 3;
    private int currentRollCost = 3;
    public Button rollButton;
    public TMP_Text rollCostText;

    public LevelData currentLevelData;
    public int currentWaveIndex;

    public bool isDoneShopping = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        panel.SetActive(false);
     
    }

    //System Shopping
    public void OpenShop()
    {
        panel.SetActive(true);
        isDoneShopping = false ;
        currentRollCost = rollCost;

        RollShopItems();
        //ClearIteams();

        //var allowedTiers = currentLevelData.waves[currentWaveIndex].allowedItemTiers;
        //var filteredItems = allItems.Where(i => allowedTiers.Contains(i.itemTier)).ToList();

        //var randomItems = filteredItems.OrderBy(i => Random.value).Take(4);
        //foreach (var item in randomItems)
        //{
            //GameObject panel = Instantiate(itemPanel, contentHolder);
            //panel.GetComponent<ItemPanelUI>().SetItem(item);
        //}
    }
    public void CloseShop()
    {
        panel.SetActive(false);
    }

    public void OnNextWaveButton()
    {
        isDoneShopping = true ;
    }

    public void ClearIteams()
    {
        foreach( Transform child in contentHolder)
        {
            Destroy(child.gameObject);
        }
    }

    // Roll Items
    public void RollShopItems()
    {
        ClearIteams();

        var allowedTiers = currentLevelData.waves[currentWaveIndex].allowedItemTiers;
        var filteredItems = allItems.Where( i => allowedTiers.Contains(i.itemTier)).ToList();

        var randomItems = filteredItems.OrderBy(i => Random.value).Take(4);

        foreach ( var item in randomItems)
        {
            GameObject panel = Instantiate(itemPanel, contentHolder);
            panel.GetComponent<ItemPanelUI>().SetItem(item);
        }

        rollCostText.text = $"Roll (-{currentRollCost})";
    }

    public void OnRollButtonPressed()
    {
        if(PlayerStats.Instance.energy >= currentRollCost)
        {
            PlayerStats.Instance.AddEnergy(-currentRollCost);
            currentRollCost *= 2;
            RollShopItems();
        }
        else
        {
            Debug.Log("Not Enough Energy");
        }
    }
}
