using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }

        // Load SFX clips from Resources folder
        LoadSFXClips();
    }

    private void LoadSFXClips()
    {
        sfxClips["shoot"] = Resources.Load<AudioClip>("Audio/SFX/shoot");
        sfxClips["explosion"] = Resources.Load<AudioClip>("Audio/SFX/explosion");
        sfxClips["gameover"] = Resources.Load<AudioClip>("Audio/SFX/gameover");
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxClips.ContainsKey(sfxName) && sfxClips[sfxName] != null)
        {
            sfxSource.PlayOneShot(sfxClips[sfxName]);
        }
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = Mathf.Clamp01(volume);
    }
}
