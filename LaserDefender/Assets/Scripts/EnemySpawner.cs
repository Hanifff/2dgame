using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfigSO> WaveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    void Start()
    {
        StartCoroutine(SpawnEnemiesWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    IEnumerator SpawnEnemiesWaves()
    {

        foreach (WaveConfigSO waveConfig in WaveConfigs)
        {
            currentWave = waveConfig;
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i),
                currentWave.GetStartingWaypoint().position,
                Quaternion.identity,
                transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpwanTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}