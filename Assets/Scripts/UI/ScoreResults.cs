using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ScoreResults : MonoBehaviour
    {
        private void Start()
        {
            var scoreText = $"YOU SCORED {GameSession.Instance.score}/{GameSession.Instance.MaximumScore}";
            var prefName = $"LEVEL {SceneManager.GetActiveScene().buildIndex}";
            
            string scoreParam = $"{prefName}HighestScore";
            
            DataController.Instance.SaveScore(scoreParam, GameSession.Instance.score);
            var menuText = $"YOU SCORED {PlayerPrefs.GetInt(scoreParam)}/{GameSession.Instance.MaximumScore}";
            DataController.Instance.SaveMenuText(prefName, menuText);

            gameObject.GetComponent<Text>().text = scoreText;
        }
    }
}