using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] AudioClip switchSound;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelection;
    public void Quit()
    {
        SoundManager.instance.PlaySound(switchSound);
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void LoadMainMenu()
    {
        PlaySwitchSound();
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void LoadNextScene()
    {
        PlaySwitchSound();
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void SoundVolume()
    {
        PlaySwitchSound();
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        PlaySwitchSound();
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    public void PlaySwitchSound()
    {
        SoundManager.instance.PlaySound(switchSound);
    }
}
