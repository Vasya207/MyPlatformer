using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Collections;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;

    GameSession gameSessionControl;

    private void Awake()
    {
        gameSessionControl = GetComponentInParent<GameSession>();
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
                pauseScreen.SetActive(false);
            else
                pauseScreen.SetActive(true);
        }
    }

    #region GameOver

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
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

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
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
