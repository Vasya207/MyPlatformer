using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("Level " + currentScene, 1);
            PlayerPrefs.Save();
            
            if(playerInput != null)
            {
                playerInput.enabled = false;
            }

            _UIManager.ActivateWinScreen();
        }
    }
}
