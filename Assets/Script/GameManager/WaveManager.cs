using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TMP_Text waveTimerText;
    [SerializeField] private TMP_Text waveText;


    private float currentWaveTime;
    private bool isWaveRunning;
    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(WaveRoutine());
    }
    private void Update()
    {
        if (isWaveRunning)
        {
            currentWaveTime -= Time.deltaTime;

            UpdateWaveTimerUI();

            if (currentWaveTime <= 0f)
            {
                EndCurrentWave();
            }
        }
    }

    private void EndCurrentWave()
    {
        isWaveRunning = false;

        DestroyAllEnemiesAndEnergy();
        UpgradeMenu.Instance.OpenShop();
        GameManager.Instance.SetState(GameState.Upgrading);
        Time.timeScale = 0f;
    }


    private void DestroyAllEnemiesAndEnergy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameObject[] energies = GameObject.FindGameObjectsWithTag("Energy");
        foreach (GameObject energy in energies)
        {
            Destroy(energy);
        }
    }

    private void UpdateWaveTimerUI()
    {
        waveTimerText.text = $"{Mathf.CeilToInt(currentWaveTime)}";
    }
    private void UpdateWaveText()
    {
        waveText.text = $"Wave {currentWave + 1}";
    }

    private IEnumerator WaveRoutine()
    {
        while (currentWave < levelData.waves.Count)
        {
            // Spawn enemy for current wave
            WaveData wave = levelData.waves[currentWave];
            UpgradeMenu.Instance.currentWaveIndex = currentWave;


            // Timer
            currentWaveTime = wave.waveDuration;
            isWaveRunning = true;
            UpdateWaveText();

            yield return new WaitForSeconds(2f);
            StartCoroutine(enemySpawner.SpawnWave(wave));

            // Wait end wave
            yield return new WaitUntil(() => isWaveRunning == false);
            EndCurrentWave();


            // Open shop
            yield return new WaitUntil(() => UpgradeMenu.Instance.isDoneShopping == true);

            UpgradeMenu.Instance.CloseShop();
            GameManager.Instance.SetState(GameState.Playing);
            Time.timeScale = 1f;

            // press Next wave
            yield return new WaitUntil(() => UpgradeMenu.Instance.isDoneShopping == true);

            // Close Shop
            UpgradeMenu.Instance.CloseShop();
            GameManager.Instance.SetState(GameState.Playing);
            Time.timeScale = 1f;

            FindAnyObjectByType<Player>()?.HealNextWave();

            currentWave++;
        }
    }

}