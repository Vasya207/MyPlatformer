using System.Collections.Generic;
using Chest;
using Enemies;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameSession : Singleton<GameSession>
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public int score;
        public int MaximumScore { get; private set; }

        private void Awake()
        {
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
            var enemyScore = FindObjectsOfType<Enemy>();

            foreach (var coin in coinScore) MaximumScore += Constants.CoinValue;

            foreach (var chest in chestScore) MaximumScore += chest.coinsInChest;

            foreach (var enemy in enemyScore) MaximumScore += enemy.rewardForKill;
        }
    }
}