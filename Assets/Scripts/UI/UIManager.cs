using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Game Over")] [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private AudioClip gameOverSound;

        [Header("Win Screen")] [SerializeField]
        private GameObject winScreen;

        [SerializeField] private AudioClip winSound;

        [Header("Pause")] [SerializeField] private GameObject pauseScreen;

        private SoundManager soundManager;
        private GameSession gameSessionControl;
        private PlayerInput playerInput;
        private Player.Player player;

        private void Awake()
        {
            gameSessionControl = GameSession.Instance;
            gameOverScreen.SetActive(false);
            winScreen.SetActive(false);
            pauseScreen.SetActive(false);
            playerInput = FindObjectOfType<PlayerInput>();
            player = FindObjectOfType<Player.Player>();
            soundManager = SoundManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !player.Dead)
            {
                if (pauseScreen.activeInHierarchy)
                    PauseGame(false);

                else
                    PauseGame(true);
            }
        }

        #region GameOver

        public void GameOver()
        {
            gameOverScreen.SetActive(true);
            soundManager.PlaySound(gameOverSound);
        }

        public void ActivateWinScreen()
        {
            winScreen.SetActive(true);
            soundManager.PlaySound(winSound);
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameSessionControl.ResetGameSession();
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameSessionControl.ResetGameSession();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
            gameSessionControl.ResetGameSession();
            PauseGame(false);
        }

        public void Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        #endregion

        #region PauseMenu

        public void PauseGame(bool status)
        {
            pauseScreen.SetActive(status);
            if (playerInput != null) playerInput.enabled = !status;

            if (status)
                Time.timeScale = 0;

            else
                Time.timeScale = 1;
        }

        public void SoundVolume()
        {
            soundManager.ChangeSoundVolume(0.2f);
        }

        public void MusicVolume()
        {
            soundManager.ChangeMusicVolume(0.2f);
        }

        #endregion
    }
}