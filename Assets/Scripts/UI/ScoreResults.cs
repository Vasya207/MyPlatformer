using UnityEngine;
using UnityEngine.UI;

public class ScoreResults : MonoBehaviour
{
    GameSession gameSession;
    private void Start()
    {
        gameSession = GetComponentInParent<GameSession>();

        string scoreText = "YOU'VE SCORED " + gameSession.score;

        gameObject.GetComponent<Text>().text = scoreText;
    }
}
