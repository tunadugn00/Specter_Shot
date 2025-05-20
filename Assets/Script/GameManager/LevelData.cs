using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName ="Wave System/Level Data")]
public class LevelData : ScriptableObject
{
    public List<WaveData> waves;
}
