using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] AudioClip switchSound;
    public void Quit()
    {
        SoundManager.instance.PlaySound(switchSound);
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void LoadNextScene()
    {
        PlaySwitchSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
