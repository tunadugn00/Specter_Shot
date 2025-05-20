using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveData
{
    public List<EnemySpawnInfo> enemiesInWave;
    public float timeBetweenSpawns = 1f;
    public float waveDuration;
    public List<ItemTier> allowedItemTiers;
}
