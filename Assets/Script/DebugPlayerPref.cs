using UnityEngine;

public class DebugPlayerPrefs : MonoBehaviour
{
    [ContextMenu("Check PlayerPrefs")]
    public void CheckPlayerPrefs()
    {
        Debug.Log("=== CHECKING PLAYERPREFS ===");
        Debug.Log("Selected Character: '" + PlayerPrefs.GetString("selected_character", "NONE") + "'");
        Debug.Log("Selected Gun: '" + PlayerPrefs.GetString("selected_gun", "NONE") + "'");
        Debug.Log("Coins: " + PlayerPrefs.GetInt("coin", 0));
        Debug.Log("Alex bought: " + PlayerPrefs.GetInt("char_bought_Alex", 0));
        Debug.Log("Lmao bought: " + PlayerPrefs.GetInt("char_bought_lmao", 0));
        Debug.Log("=============================");
    }

    [ContextMenu("Clear All PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Cleared all PlayerPrefs");
    }

    [ContextMenu("Set Test Data")]
    public void SetTestData()
    {
        PlayerPrefs.SetString("selected_character", "lmao");
        PlayerPrefs.SetInt("char_bought_Alex", 1);
        PlayerPrefs.SetInt("char_bought_lmao", 1);
        PlayerPrefs.SetInt("coin", 1000);
        PlayerPrefs.Save();
        Debug.Log("Set test data");
    }
}