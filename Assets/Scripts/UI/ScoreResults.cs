using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ScoreResults : MonoBehaviour
    {
        private GameSession gameSession;

        private void Start()
        {
            gameSession = GetComponentInParent<GameSession>();

            var scoreText = "YOU SCORED " + gameSession.score + "/" + gameSession.MaximumScore;
            var prefName = "Level".ToUpper() + " " + SceneManager.GetActiveScene().buildIndex;

            if (PlayerPrefs.GetInt(prefName + "HS") < gameSession.score)
                PlayerPrefs.SetInt(prefName + "HS", gameSession.score);

            var menuText = "YOU SCORED " + PlayerPrefs.GetInt(prefName + "HS") + "/" + gameSession.MaximumScore;

            PlayerPrefs.SetString(prefName, menuText);
            PlayerPrefs.Save();

            gameObject.GetComponent<Text>().text = scoreText;
        }
    }
}