#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class UpgradeItemGenerator
{
    [MenuItem("Tools/Generate Sample Upgrade Items")]
    public static void GenerateUpgradeItems()
    {
        string rootFolder = "Assets/Script/Upgrades";
        string folderPath = rootFolder + "/Items";

        // Tạo folder nếu chưa có
        if (!AssetDatabase.IsValidFolder(rootFolder))
        {
            AssetDatabase.CreateFolder("Assets/Script", "Upgrades");
        }
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder(rootFolder, "Items");
        }

        string[] statNames = new string[]
        {
            "damage", "maxHP", "fireRate", "moveSpeed", "armor", "lifeSteal",
            "regenHP", "critRate", "critDamage", "knockback"
        };

        for (int i = 0; i < statNames.Length; i++)
        {
            UpgradeItem newItem = ScriptableObject.CreateInstance<UpgradeItem>();
            newItem.itemName = "Upgrade " + statNames[i];
            newItem.description = "Tăng " + statNames[i] + " thêm " + (i + 1);
            newItem.price = Random.Range(5, 20);
            newItem.statToUpgrade = statNames[i];
            newItem.upgradeAmount = (statNames[i].Contains("rate") || statNames[i].Contains("life") || statNames[i].Contains("crit"))
                ? 0.1f + i * 0.05f
                : 5 + i * 2;

            string assetPath = $"{folderPath}/Upgrade_{statNames[i]}.asset";
            AssetDatabase.CreateAsset(newItem, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("✅ Đã tạo UpgradeItems trong: " + folderPath);
    }
}
#endif
