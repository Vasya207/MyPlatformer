using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public int score = 0;
    public string levelName;

    public int generalScore { get; private set; }

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        ScoreManager();
    }

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ResetGameSession()
    {
        Destroy(gameObject);
    }

    private void ScoreManager()
    {
        generalScore = 0;

        Coin[] coinScore = FindObjectsOfType<Coin>();
        ChestLogic[] chestScore = FindObjectsOfType<ChestLogic>();
        MeleeEnemy[] enemyScore = FindObjectsOfType<MeleeEnemy>();

        foreach (Coin coin in coinScore)
        {
            generalScore += coin.pointsForCoinPickup;
        }
        foreach (ChestLogic chest in chestScore)
        {
            generalScore += chest.coinsInChest;
        }
        foreach (MeleeEnemy enemy in enemyScore)
        {
            generalScore += enemy.rewardForKill;
        }

        //Debug.Log(generalScore);
    }
}
