using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public CharacterData[] allCharacters;
    public GunData[] allGuns;
    public Transform gunAttackPoint;

    private void Awake()
    {
        Debug.Log("=== PLAYER LOADER START ===");


        string charId = PlayerPrefs.GetString("selected_character", "Alex");
        string gunId = PlayerPrefs.GetString("selected_gun", allGuns.Length > 0 ? allGuns[0].id : "");


        // Tìm character data
        CharacterData charData = null;
        for (int i = 0; i < allCharacters.Length; i++)
        {
            if (allCharacters[i].id == charId)
            {
                charData = allCharacters[i];
                break;
            }
        }

        // Tìm gun data
        GunData gunData = null;
        for (int i = 0; i < allGuns.Length; i++)
        {
            if (allGuns[i].id == gunId)
            {
                gunData = allGuns[i];
                break;
            }
        }

        Debug.Log($"Found Character: {(charData != null ? charData.name + " (ID: " + charData.id + ")" : "NULL")}");
        Debug.Log($"Found Gun: {(gunData != null ? gunData.name + " (ID: " + gunData.id + ")" : "NULL")}");

        // Fallback nếu không tìm thấy
        if (charData == null)
        {
            charData = allCharacters[0];
            Debug.LogWarning($"Character '{charId}' not found! Using default: {charData.name} (ID: {charData.id})");
        }

        if (gunData == null)
        {
            gunData = allGuns[0];
            Debug.LogWarning($"Gun '{gunId}' not found! Using default: {gunData.name} (ID: {gunData.id})");
        }

        // Initialize player
        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.InitCharacter(charData, gunData);
            Debug.Log($"Initialized player with: {charData.name} + {gunData.name}");
        }
        else
        {
            Debug.LogError("PlayerStats component not found!");
        }

        // Change character sprite/prefab
        ChangeCharacterVisual(charData);

        // Spawn gun
        if (gunData.prefab != null && gunAttackPoint != null)
        {
            GameObject gunInstance = Instantiate(gunData.prefab, gunAttackPoint);
            Debug.Log($"Gun spawned: {gunInstance.name}");
        }
        else
        {
            if (gunData.prefab == null) Debug.LogError($"Gun prefab is null for {gunData.id}");
            if (gunAttackPoint == null) Debug.LogError("Gun Attack Point is null");
        }

        Debug.Log("=== PLAYER LOADER END ===");
    }

    private void ChangeCharacterVisual(CharacterData charData)
    {
        // Tìm SpriteRenderer của player
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        if (playerSprite == null)
        {
            playerSprite = GetComponentInChildren<SpriteRenderer>();
        }

        if (playerSprite != null && charData.prefab != null)
        {
            // Nếu CharacterData có prefab, instantiate prefab mới
            Debug.Log($"Changing to character prefab: {charData.name}");

            // Xóa visual cũ (nếu có)
            Transform oldVisual = transform.Find("CharacterVisual");
            if (oldVisual != null)
            {
                DestroyImmediate(oldVisual.gameObject);
            }

            // Tạo visual mới
            GameObject newVisual = Instantiate(charData.prefab, transform);
            newVisual.name = "CharacterVisual";
            newVisual.transform.localPosition = Vector3.zero;

            // Disable các component không cần thiết trong visual (như Collider, Rigidbody)
            Collider2D[] colliders = newVisual.GetComponentsInChildren<Collider2D>();
            foreach (var col in colliders)
            {
                col.enabled = false;
            }

            Rigidbody2D[] rigidbodies = newVisual.GetComponentsInChildren<Rigidbody2D>();
            foreach (var rb in rigidbodies)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
        else if (playerSprite != null && charData.icon != null)
        {
            // Fallback: chỉ đổi sprite
            Debug.Log($"Changing to character sprite: {charData.name}");
            playerSprite.sprite = charData.icon;
        }
        else
        {
            Debug.LogWarning("Cannot change character visual - no SpriteRenderer or character data");
        }
    }
}