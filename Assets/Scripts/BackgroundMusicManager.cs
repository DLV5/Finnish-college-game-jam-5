using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private static BackgroundMusicManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // We will handle looping manually
        PlayNextClip();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    private void PlayNextClip()
    {
        if (currentClipIndex == 0)
        {
            audioSource.clip = clip1;
        }
        else
        {
            audioSource.clip = clip2;
        }

        audioSource.Play();
        currentClipIndex = 1 - currentClipIndex; // Toggle between 0 and 1
    }
}
