using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public IEnumerator SpawnWave(WaveData waveData)
    {
        foreach (var enemyInfo in waveData.enemiesInWave)
        {
            for (int i = 0; i < enemyInfo.spawnCount; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyInfo.enemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(waveData.timeBetweenSpawns);
            }
        }
    }


}
