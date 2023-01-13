using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    AudioSource soundSource;
    AudioSource musicSource;

    private void Awake()
    {
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        soundSource = GetComponent<AudioSource>();

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if(instance != null && instance != this)
            Destroy(gameObject);

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }

    public void ChangeSoundVolume(float change)
    {
        ChangeSourceVolume(1, "soundVolume", change, soundSource);
    }

    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.15f, "musicVolume", change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        //Check if we reached the maximum or minimum value
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
