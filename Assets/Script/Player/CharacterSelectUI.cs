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

        bool isBought = PlayerPrefs.GetInt("char_bought" + charData.id, 0) == 1;

        selectButton.gameObject.SetActive(isBought);
        buyButton.gameObject.SetActive(!isBought);

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("selected_character", characterData.id);
            PlayerPrefs.Save();
            Debug.Log("Đã chọn nv" + characterData.name);
        });

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() =>
        {
            int coin = PlayerPrefs.GetInt("coin", 0);
            if (coin >= characterData.price)
            {
                PlayerPrefs.SetInt("coin", coin - characterData.price);
                PlayerPrefs.SetInt("char_bought" + characterData.id, 1);
                PlayerPrefs.SetString("selected_character", characterData.id);
                PlayerPrefs.Save();

                selectButton.gameObject.SetActive(true);
                buyButton.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Không đủ coin");
            }
        });
    }
}
