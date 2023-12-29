using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    public AudioSource source2;
    PlayerController player; 
    public static AudioManager Instance { get; private set; }

    public bool startLandingSound;

    public float volume = 1;
    public float landingSound = 1;
    public float jumpingVolume = 1;
    public float deathVolume = 1;
    public float switchVolume = 1;
    public float backgroundMusicVolume = 1;
    public float collectibleVolume = 1;


    public AudioClip jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium, death, backgroundMusic, collectible;
    public List<AudioClip> clips;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
        player = PlayerController.instance;

        PlayingBackgorundM();
    }

    

    private void Update()
    {
    }
    public void PlayingAudio(AudioClip clip, float volume)
    {
       source.clip = clip;
       source.volume = volume;
        source.PlayOneShot(clip, volume);

    }

    private void PlayingBackgorundM()
    {
        source2.clip = backgroundMusic;
        source2.volume = backgroundMusicVolume;
        source2.PlayOneShot(backgroundMusic, backgroundMusicVolume);
    }
}
