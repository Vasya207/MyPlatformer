using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
            Destroy(gameObject);
    }
    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
