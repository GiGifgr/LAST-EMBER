using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;

    private void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        musicSource.Stop();

        musicSource.clip = mainMenuMusic;
        musicSource.loop = true;

        musicSource.Play();
    }

    public void PlayGameplayMusic()
    {
        musicSource.Stop();

        musicSource.clip = gameplayMusic;
        musicSource.loop = true;

        musicSource.Play();
    }

    public void PauseGameplayMusic()
    {
        musicSource.Pause();
    }

    public void ContinueGameplayMusic()
    {
        musicSource.UnPause();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}