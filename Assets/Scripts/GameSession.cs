using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int enemiesDestroyedForPowerUp;

    [SerializeField] List<PowerUp> powerUps;
    [SerializeField] int minEnemiesForPowerUp = 20;
    [SerializeField] int maxEnemiesForPowerUp = 40;
    private void Awake()
    {
        SetUpSingleton();   
    }
    private void Start()
    {
        enemiesDestroyedForPowerUp = Random.Range(this.minEnemiesForPowerUp, this.maxEnemiesForPowerUp);
    }
    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberGameSessions>1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public PowerUp EnemyDestroyed(int count)
    {
        this.enemiesDestroyedForPowerUp-=count;
        if(enemiesDestroyedForPowerUp<=0)
        {
            enemiesDestroyedForPowerUp = Random.Range(this.minEnemiesForPowerUp, this.maxEnemiesForPowerUp);
            return this.powerUps[Random.Range(0, powerUps.Count)];
        }
        return null;
    }
public int GetScore()
    {
        return score;
    }
    public void AddToScore(int scoreValue)
    {
        this.score += scoreValue;
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
