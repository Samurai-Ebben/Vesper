using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    public AudioSource source2;
    public AudioSource source3;
    public static AudioManager Instance { get; private set; }

    public bool startLandingSound;

    public float landingVolume = 0.5f;
    public float jumpingVolume = 0.5f;
    public float deathVolume = 0.5f;
    public float switchVolume = 0.5f;
    public float collectibleVolume = 0.5f;
    public float destructiblePlatfromVolume = 0.5f;
    public float disappearingPlatformVolume = 0.5f;
    public float trampolineJumpVolume = 0.5f;
    public float clickInMenuVolume = 0.5f;
    public float risingPlatformVolume = 0.5f;
    public float appearingPlatformVolume = 0.5f;
    public float powerUpVolume = 0.5f;
    public float powerUpSmallVolume = 0.5f;
    public float powerUpLargeVolume = 0.5f;


    public float pauseMenuVolume = 0.5f;

    public float backgroundMusicOneVolume = 0.5f;
    public float backgroundMusicTwoVolume = 0.5f;

    public List<string> caveLevels;
    public List<string> surfaceLevels;

    public List<string> muteLevels;

    float backgorundVlume = 1;
    public bool isInLevelTwo;

    bool sceneIsMuted;

    string currentScene;


    public AudioClip jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium, death, collectible, pauseMenu, clickInMenu
        , destructiblePlatfrom, disappearingPlatformSound, appearingPlatformSound, trampolineJump, risingPlatformSound, powerUpSmallSound, powerUpLargeSound, backgroundMusic, backgroundMusic2;
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
        //PlayingBackgorundM();
        source2.loop = true;
    }



    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        SwapMusic();
        MuteScenes();

    }

    private void MuteScenes()
    {
        for (int i = 0; i < muteLevels.Count; i++)
        {
            if (muteLevels[i] == currentScene)
            {
                source2.Stop();
            }
        }

        if (!source2.isPlaying)
        {
            source2.Play();
        }
        else if (sceneIsMuted == true)
        {
            source2.Stop();
        }
    }

    private void SwapMusic()
    {

        for (int i = 0; i < caveLevels.Count; i++)
        {
            if (caveLevels[i] == currentScene)
            {
                source2.clip = backgroundMusic;
                source2.volume = backgroundMusicOneVolume;
            }
        }

        for (int i = 0; i < surfaceLevels.Count; i++)
        {
            if (surfaceLevels[i] == currentScene)
            {
                source2.clip = backgroundMusic2;
                source2.volume = backgroundMusicTwoVolume;
            }
        }
    }

    public void GameplaySFX(AudioClip clip, float volume = 1)
    {
        source.clip = clip;
        source.volume = volume;
        source.PlayOneShot(clip, volume);

    }

    private void PlayingBackgorundM()
    {
        if (!source2.isPlaying)
        {
            source2.clip = backgroundMusic;
            source2.Play();
        }
        source2.clip = backgroundMusic;
        source2.Play();
    }

    public void MenuSFX(AudioClip clip, float volume = 1)
    {
        source3.clip = clip;
        source3.volume = volume;
        source3.PlayOneShot(clip);
    }
}
