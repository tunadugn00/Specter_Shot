using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject characterSelectPrefabs;
    public CharacterData[] allCharacter;

    private int currentIndex = 0;
    private CharacterSelectUI currentUI; // Thay đổi từ GameObject thành CharacterSelectUI

    private void Start()
    {
        // Tạo UI một lần duy nhất
        GameObject uiObject = Instantiate(characterSelectPrefabs, contentPanel);
        currentUI = uiObject.GetComponent<CharacterSelectUI>();

        ShowCharacter(currentIndex);
    }

    public void ShowNext()
    {
        currentIndex = (currentIndex + 1) % allCharacter.Length;
        ShowCharacter(currentIndex);
    }

    public void ShowPrevious()
    {
        currentIndex = (currentIndex - 1 + allCharacter.Length) % allCharacter.Length;
        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        // Chỉ update UI thay vì destroy/create
        if (currentUI != null)
        {
            currentUI.Setup(allCharacter[index]);
        }
    }
}