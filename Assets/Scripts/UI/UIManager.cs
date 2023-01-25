using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Collections;
using UnityEngine.InputSystem;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] AudioClip gameOverSound;

    [Header("Win Screen")]
    [SerializeField] GameObject winScreen;
    [SerializeField] AudioClip winSound;

    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;

    GameSession gameSessionControl;
    PlayerInput playerInput;
    Player player;

    private void Awake()
    {
        gameSessionControl = GetComponentInParent<GameSession>();
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        pauseScreen.SetActive(false);
        playerInput = FindObjectOfType<PlayerInput>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !player.dead)
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }

            else
            {
                PauseGame(true);
            }
        }
    }

    #region GameOver

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
        SoundManager.instance.PlaySound(winSound);
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
        if(playerInput != null)
        {
            playerInput.enabled = !status;
        }

        if (status)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion
}
