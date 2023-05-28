using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Core
{
    public class FinishLevel : MonoBehaviour
    {
        private PlayerInput playerInput;
        private UIManager uiManager;

        private void Awake()
        {
            uiManager = UIManager.Instance;
            playerInput = FindObjectOfType<PlayerInput>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var currentScene = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt($"Level {currentScene}", 1);
                PlayerPrefs.Save();

                if (playerInput != null) playerInput.enabled = false;

                uiManager.ActivateWinScreen();
            }
        }
    }
}