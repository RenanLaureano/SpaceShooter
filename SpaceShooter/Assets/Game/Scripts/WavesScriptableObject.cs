using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Game/WavesConfig", order = 1)]
public class WavesScriptableObject : ScriptableObject
{
    public List<WaveObejct> waves;
}

[Serializable]
public class WaveObejct
{
    public int timeToStartWave;
    public float timeToSpawn;
    public int killsGoal;
    public int lifeRecive;
    public BaseShip[] enemies;
}
