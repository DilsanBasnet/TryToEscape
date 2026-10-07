using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip coinCollectSFX;
    public AudioClip deathSFX;
    public AudioClip portalSFX;
    public AudioClip jumpPadSFX;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;

        }
    }

    private void Start()
    {
        PlayBackgroundMusic();

    }

    public void PlayBackgroundMusic()
    {
        if(musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = 0.1f;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if(sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
