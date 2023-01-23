using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLelvel : MonoBehaviour
{
    GameSession gameSession;
    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetInt("currentScene", PlayerPrefs.GetInt("currentScene") + 1);
            SceneManager.LoadScene(1);

            gameSession.ResetGameSession();
        }
    }
}
