using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreResults : MonoBehaviour
{
    GameSession gameSession;
    private void Start()
    {
        gameSession = GetComponentInParent<GameSession>();

        string scoreText = "YOU SCORED " + gameSession.score + "/" + gameSession.generalScore;
        string prefName = "Level".ToUpper() + " " + SceneManager.GetActiveScene().buildIndex;
        
        if(PlayerPrefs.GetInt(prefName + "HS") < gameSession.score)
        {
            PlayerPrefs.SetInt(prefName + "HS", gameSession.score);
        }

        string menuText = "YOU SCORED " + PlayerPrefs.GetInt(prefName + "HS") + "/" + gameSession.generalScore;

        PlayerPrefs.SetString(prefName, menuText);
        PlayerPrefs.Save();

        gameObject.GetComponent<Text>().text = scoreText;
    }
}
