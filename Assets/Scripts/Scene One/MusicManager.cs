using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource musicSource;
    [Range(0f, 1f)]
    public float volume = 1f;

    private void Awake()
    {
        // Singleton Pattern to persist music across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlayMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.volume = volume;
            musicSource.Play();
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
}
