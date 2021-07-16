using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] Transform[] SpawnPositions;
    BaseShip[] EnemiesPrefab;
    float TimeSpawn;

    float contSpawn;
    bool canSpawn = false;

    private void Awake()
    {
        TriggerService.Instance.AddListener(TriggerType.START_ROUND, ChangeState);
        TriggerService.Instance.AddListener(TriggerType.END_ROUND, ChangeState);
        TriggerService.Instance.AddListener(TriggerType.GAME_OVER, ChangeState);
    }

    private void ChangeState(TriggerType triggerType, object param)
    {
        switch (triggerType)
        {
            case TriggerType.START_ROUND:

                WaveObejct wave = (WaveObejct)param;

                EnemiesPrefab = wave.enemies;
                TimeSpawn = wave.timeToSpawn;

                canSpawn = true;

                break;
            case TriggerType.END_ROUND:
                canSpawn = false;
                break;
            case TriggerType.GAME_OVER:
                canSpawn = false;
                break;
        }

       
    }

    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        contSpawn += Time.deltaTime;

        if(contSpawn >= TimeSpawn)
        {
            int indexEnemy = Random.Range(0, EnemiesPrefab.Length);
            int indexSpawn = Random.Range(0, SpawnPositions.Length);

            Instantiate(EnemiesPrefab[indexEnemy], SpawnPositions[indexSpawn].position, SpawnPositions[indexSpawn].rotation);

            contSpawn = 0;
        }
    }
}
