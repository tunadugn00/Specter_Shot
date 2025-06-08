using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject characterSelectPrefabs;
    public CharacterData[] allCharacter;

    private int currentIndex = 0;
    private GameObject currentItemUI;

    private void Start()
    {
        ShowCharacter(currentIndex);
    }

    public void ShowNext()
    {
        currentIndex = (currentIndex +1) % allCharacter.Length;
        ShowCharacter(currentIndex);
    }
    public void ShowPrevious()
    {
        currentIndex = (currentIndex -1 + allCharacter.Length) % allCharacter.Length;
        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        //delete old item
        if (currentItemUI != null) { Destroy(currentItemUI); }
        
        //new item
        currentItemUI = Instantiate(characterSelectPrefabs, contentPanel);
        CharacterSelectUI ui = currentItemUI.GetComponent<CharacterSelectUI>();
        ui.Setup(allCharacter[index]);
    }
}
