using UnityEngine;

public class GunSelectManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject gunSelectPrefabs;
    public GunData[] allGun;

    private int currentIndex = 0;
    private GameObject currentItemUI;

    private void Start()
    {
        ShowGun(currentIndex);
    }

    public void ShowNext()
    {
        currentIndex = (currentIndex +1) % allGun.Length;
        ShowGun(currentIndex);
    }
    public void ShowPrevious()
    {
        currentIndex = (currentIndex -1 + allGun.Length) % allGun.Length;
        ShowGun(currentIndex);
    }

    private void ShowGun(int index)
    {
        //delete old item
        if (currentItemUI != null) { Destroy(currentItemUI); }
        
        //new item
        currentItemUI = Instantiate(gunSelectPrefabs, contentPanel);
        GunSelectUI ui = currentItemUI.GetComponent<GunSelectUI>();
        ui.Setup(allGun[index]);
    }
}
