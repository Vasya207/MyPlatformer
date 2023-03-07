using System.Collections.Generic;
using Chest;
using Enemies;
using TMPro;
using UnityEngine;

namespace Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public int score;
        public int MaximumScore { get; private set; }

        private void Awake()
        {
            //Switch to singleton
            //Create one base class Singleton
            var numGameSessions = FindObjectsOfType<GameSession>().Length;
            if (numGameSessions > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);

            ManageScore();
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

        private void ManageScore()
        {
            MaximumScore = 0;
            var coinScore = FindObjectsOfType<Coin.Coin>();
            var chestScore = FindObjectsOfType<ChestLogic>();
            var enemyScore = FindObjectsOfType<MeleeEnemy>();

            foreach (var coin in coinScore) MaximumScore += coin.pointsForCoinPickup;

            foreach (var chest in chestScore) MaximumScore += chest.coinsInChest;

            foreach (var enemy in enemyScore) MaximumScore += enemy.rewardForKill;
        }
        
    }
}