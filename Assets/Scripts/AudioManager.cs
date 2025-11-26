using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Source")]
    public AudioSource sfxSource;
    
    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float sfxVolume = 0.8f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // Set initial volume
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }
    
    // Sound effects methods
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    
    public void PlaySFX(AudioClip clip, float volume)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }
    
    // Volume control methods
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }
    
    // Utility methods
    public bool IsSFXPlaying()
    {
        return sfxSource != null && sfxSource.isPlaying;
    }
}
