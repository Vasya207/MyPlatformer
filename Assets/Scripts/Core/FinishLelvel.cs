using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FinishLelvel : MonoBehaviour
{
    UIManager _UIManager;
    PlayerInput playerInput;
    private void Awake()
    {
        _UIManager = FindObjectOfType<UIManager>();
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetInt("currentScene", PlayerPrefs.GetInt("currentScene") + 1);
            if(playerInput != null)
            {
                playerInput.enabled = false;
            }

            _UIManager.ActivateWinScreen();
        }
    }
}
