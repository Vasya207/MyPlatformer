using Chest;
using TMPro;
using UnityEngine;

namespace Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public int score;
        public int GeneralScore { get; private set; }

        private void Awake()
        {
            var numGameSessions = FindObjectsOfType<GameSession>().Length;
            if (numGameSessions > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);

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
            GeneralScore = 0;

            var coinScore = FindObjectsOfType<Coin.Coin>();
            var chestScore = FindObjectsOfType<ChestLogic>();
            var enemyScore = FindObjectsOfType<MeleeEnemy>();

            foreach (var coin in coinScore) GeneralScore += coin.pointsForCoinPickup;

            foreach (var chest in chestScore) GeneralScore += chest.coinsInChest;

            foreach (var enemy in enemyScore) GeneralScore += enemy.rewardForKill;
        }
    }
}