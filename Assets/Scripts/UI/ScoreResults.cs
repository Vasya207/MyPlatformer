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
            var scoreText = "YOU SCORED " + GameSession.Instance.score + "/" + GameSession.Instance.MaximumScore;
            var prefName = "Level".ToUpper() + " " + SceneManager.GetActiveScene().buildIndex;

            if (PlayerPrefs.GetInt(prefName + "HS") < GameSession.Instance.score)
                PlayerPrefs.SetInt(prefName + "HS", GameSession.Instance.score);

            var menuText = "YOU SCORED " + PlayerPrefs.GetInt(prefName + "HS") + "/" + GameSession.Instance.MaximumScore;

            PlayerPrefs.SetString(prefName, menuText);
            PlayerPrefs.Save();

            gameObject.GetComponent<Text>().text = scoreText;
        }
    }
}