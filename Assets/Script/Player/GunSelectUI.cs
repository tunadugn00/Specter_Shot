using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public Button selectButton;
    public Button buyButton;

    [HideInInspector] public GunData gunData;

    public void Setup(GunData data)
    {
        gunData = data;

        iconImage.sprite = gunData.icon;
        nameText.text = gunData.name;

        bool isBought = PlayerPrefs.GetInt("gun_bought" + data.id, 0) == 1;

        selectButton.gameObject.SetActive(isBought);
        buyButton.gameObject.SetActive(!isBought);

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("selected_gun", gunData.id);
            PlayerPrefs.Save();
            Debug.Log("Đã chọn " + gunData.name);
        });

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() =>
        {
            int coin = PlayerPrefs.GetInt("coin", 0);
            if (coin >= gunData.price)
            {
                PlayerPrefs.SetInt("coin", coin - gunData.price);
                PlayerPrefs.SetInt("gun_bought" + gunData.id, 1);
                PlayerPrefs.SetString("selected_gun", gunData.id);
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
