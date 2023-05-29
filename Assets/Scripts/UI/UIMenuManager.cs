using Core;
using UnityEngine;

namespace UI
{
    public class UIMenuManager : MonoBehaviour
    {
        [SerializeField] AudioClip switchSound;
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject levelSelection;

        private SoundManager soundManager;
        
        private void Awake()
        {
            levelSelection.SetActive(false);
            mainMenu.SetActive(true);
            soundManager = SoundManager.Instance;
        }
        public void Quit()
        {
            soundManager.PlaySound(switchSound);
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
            soundManager.ChangeSoundVolume(0.2f);
        }

        public void MusicVolume()
        {
            PlaySwitchSound();
            soundManager.ChangeMusicVolume(0.2f);
        }

        public void PlaySwitchSound()
        {
            soundManager.PlaySound(switchSound);
        }
    }
}
