using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public Button selectButton;
    public Button buyButton;

    [HideInInspector] public CharacterData characterData;

    public void Setup(CharacterData charData)
    {
        characterData = charData;

        iconImage.sprite = characterData.icon;
        nameText.text = characterData.name;

        // Đảm bảo key consistent (dùng lowercase hoặc exact case)
        string buyKey = "char_bought_" + charData.id;
        bool isBought = PlayerPrefs.GetInt(buyKey, charData.id == "Alex" ? 1 : 0) == 1;

        Debug.Log($"Setup character {charData.id}: bought={isBought}, key={buyKey}");

        selectButton.gameObject.SetActive(isBought);
        buyButton.gameObject.SetActive(!isBought);

        // Clear listeners trước
        selectButton.onClick.RemoveAllListeners();
        buyButton.onClick.RemoveAllListeners();

        // Add listeners
        if (isBought)
        {
            selectButton.onClick.AddListener(() => SelectCharacter());
        }

        buyButton.onClick.AddListener(() => BuyCharacter());
    }

    private void SelectCharacter()
    {
        Debug.Log($"BEFORE SELECT: Current selected = '{PlayerPrefs.GetString("selected_character", "NONE")}'");

        PlayerPrefs.SetString("selected_character", characterData.id);
        PlayerPrefs.Save();

        Debug.Log($"AFTER SELECT: Set selected to '{characterData.id}'");
        Debug.Log($"VERIFY: PlayerPrefs now has '{PlayerPrefs.GetString("selected_character", "NONE")}'");
    }

    private void BuyCharacter()
    {
        int coin = PlayerPrefs.GetInt("coin", 0);
        if (coin >= characterData.price)
        {
            PlayerPrefs.SetInt("coin", coin - characterData.price);
            PlayerPrefs.SetInt("char_bought_" + characterData.id, 1);
            PlayerPrefs.Save();

            Debug.Log($"Bought character {characterData.id}");

            // Refresh UI
            Setup(characterData);

            // Auto select after buying
            SelectCharacter();
        }
        else
        {
            Debug.Log($"Không đủ coin: có {coin}, cần {characterData.price}");
        }
    }
}