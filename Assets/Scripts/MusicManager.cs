using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume = 1f; // valor inicial del volumen

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // persiste entre escenas

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = volume;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    /// <summary>
    /// Cambia el volumen de la música
    /// </summary>
    /// <param name="newVolume">Valor entre 0 y 1</param>
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (audioSource != null)
            audioSource.volume = volume;
    }

    /// <summary>
    /// Pausa la música
    /// </summary>
    public void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Pause();
    }

    /// <summary>
    /// Reanuda la música
    /// </summary>
    public void ResumeMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }
}
