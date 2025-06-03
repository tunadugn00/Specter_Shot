using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public IEnumerator SpawnWave(WaveData waveData)
    {
        if (waveData == null || waveData.enemiesInWave == null || waveData.enemiesInWave.Count == 0)
            yield break;

        if (spawnPoints == null || spawnPoints.Length == 0)
            yield break;

        foreach (var enemyInfo in waveData.enemiesInWave)
        {
            if (enemyInfo == null || enemyInfo.enemyPrefab == null)
                continue;

            for (int i = 0; i < enemyInfo.spawnCount; i++)
            {
                Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyInfo.enemyPrefab, point.position, Quaternion.identity);
                yield return new WaitForSeconds(waveData.timeBetweenSpawns);
            }
        }
    }
}
