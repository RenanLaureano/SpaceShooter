using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    enum GameState { 
        STARTING_ROUND,
        IN_GAME,
        GAME_OVER,
    }


    [SerializeField] private Text EnemiesKilled;
    [SerializeField] private Text RoundText;
    [SerializeField] private Text RoundCount;
    [SerializeField] private Transform LifeBar;

    [SerializeField] private WavesScriptableObject wavesConfig;

    private GameState gameState;
    private int kills = 0;
    private int killsGoal = 25;
    private int round;

    private void Awake()
    {
        TriggerService.Instance.AddListener(TriggerType.KILL_ENEMY, EnemyKilled);
        TriggerService.Instance.AddListener(TriggerType.CHANGE_LIFE, UpdateLifeBar);
        TriggerService.Instance.AddListener(TriggerType.END_ROUND, ChangeRound);
        TriggerService.Instance.AddListener(TriggerType.GAME_OVER, ClearTriggers);
    }

    private void ClearTriggers(TriggerType type, object param)
    {
        TriggerService.Instance.ClearListeners();
    }

    void Start()
    {
        RoundCount.gameObject.SetActive(false);

        IgnoreLayers();

        StartCoroutine(StartNewRound(wavesConfig.waves[round]));
    }

    void Update()
    {
        
    }

    void UpdateLifeBar(TriggerType trigger, object param)
    {
        float perc = (float)param;

        if (perc <= 0)
        {
            perc = 0;
            gameState = GameState.GAME_OVER;
        }

        LifeBar.localScale = new Vector3(perc, 1, 1);
    }

    void ChangeRound(TriggerType trigger, object param)
    {
        round++;

        kills = 0;

        gameState = GameState.STARTING_ROUND;

        if (round >= wavesConfig.waves.Count)
        {
            TriggerService.Instance.FireEvent(TriggerType.GAME_OVER, true);
            return;
        }

        StartCoroutine(StartNewRound(wavesConfig.waves[round]));
    }

    IEnumerator StartNewRound(WaveObejct wave)
    {
        int timeToStart = wave.timeToStartWave;
        RoundCount.text = "Game start in " + timeToStart.ToString();
        RoundCount.gameObject.SetActive(true);

        killsGoal = wave.killsGoal;
        EnemiesKilled.text = string.Format("{0}/{1}", kills, killsGoal);
        
        while (timeToStart > 0)
        {
            yield return new WaitForSeconds(1f);
            timeToStart--;
            RoundCount.text = "Game start in " + timeToStart.ToString();
        }

        RoundCount.gameObject.SetActive(false);
        RoundText.text = "Round " + (round + 1).ToString();
        gameState = GameState.IN_GAME;
        TriggerService.Instance.FireEvent(TriggerType.START_ROUND, wave);
    }

    void EnemyKilled(TriggerType trigger, object param)
    {

        if(gameState != GameState.IN_GAME)
        {
            return;
        }

        kills++;
        EnemiesKilled.text = string.Format("{0}/{1}", kills, killsGoal);

        if(kills >= killsGoal)
        {
            TriggerService.Instance.FireEvent(TriggerType.END_ROUND, null);
        }
    }

    void IgnoreLayers()
    {
        Physics2D.IgnoreLayerCollision(6, 6);
    }
}
