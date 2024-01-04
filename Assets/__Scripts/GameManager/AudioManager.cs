using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

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

    public float fadeOutSpeed = 0.005f;
    public float fadeInSpeed = 0.005f;
    public float timeToStopMusic = 3;



    public List<string> caveLevels;
    public List<string> surfaceLevels;

    public List<string> muteLevels;

    float backgorundVlume = 1;
    public bool isInLevelTwo;

    bool sceneIsMuted;

    public bool fadingIn;
    public bool fadingOut;

    float fadeTest;

    float volume = 0;
    float backgroundMusicVolume;

    string currentScene;

    float timer;


    public AudioClip jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium, death, collectible, pauseMenu, clickInMenu
        , destructiblePlatfrom, disappearingPlatformSound, appearingPlatformSound, trampolineJump, risingPlatformSound, powerUpSmallSound, powerUpLargeSound, backgroundMusic1, backgroundMusic2;
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

        source2.volume = 0;
    }



    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        //backgroundMusicTwoVolume = Mathf.Lerp(backgroundMusicTwoVolume, 0.5f, 0.001f);
        //source2.volume = backgroundMusicTwoVolume;


        SwapMusic();
        MuteScenes();
        if (fadingOut)
        {
            StopAllCoroutines();
            FakeFadeOut();
        }

    }

    private void FakeFadeOut()
    {
        backgroundMusicVolume -= 0.1f * Time.deltaTime;
        source2.volume = backgroundMusicVolume;
    }

    private void SwapMusic()
    {

        for (int i = 0; i < caveLevels.Count; i++)
        {
            if (caveLevels[i] == currentScene)
            {
                timer = 0;
                backgroundMusicVolume = backgroundMusicOneVolume;
                source2.clip = backgroundMusic1;

                fadingIn = true;
                fadingOut = false;

                if (fadingIn)
                {
                    StartCoroutine(FadeIn(backgroundMusicVolume, 0, fadeInSpeed));
                }

            }
        }

        for (int i = 0; i < surfaceLevels.Count; i++)
        {
            if (surfaceLevels[i] == currentScene)
            {
                timer = 0;
                backgroundMusicVolume = backgroundMusicTwoVolume;
                source2.clip = backgroundMusic2;
                fadingIn = true;
                fadingOut = false;
                if (fadingIn)
                {
                    StartCoroutine(FadeIn(backgroundMusicVolume, 0, fadeInSpeed));
                }
                Debug.Log(backgroundMusicVolume);
            }
        }
    }

    private void MuteScenes()
    {
        for (int i = 0; i < muteLevels.Count; i++)
        {
            if (muteLevels[i] == currentScene)
            {
                fadingIn = false;
                fadingOut = true;

                Debug.Log(backgroundMusicVolume);
                if (fadingOut)
                {
                    ////StopCoroutine(FadeIn(backgroundMusicVolume, 0, fadeInSpeed));
                    //StopAllCoroutines();    
                    //StartCoroutine(FadeOut(backgroundMusicVolume, fadeOutSpeed));


                }


                //if (timer >= timeToStopMusic)
                //{
                //    source2.Stop();
                //}

            }
        }

        if (!source2.isPlaying)
        {
            //source2.volume = backgroundMusicVolume;
            source2.Play();
        }

    }



    public void GameplaySFX(AudioClip clip, float volume = 1, bool audioVariaion = false, float variationRange = 0.5f)
    {
        if (audioVariaion)
        {

        }
        source.clip = clip;
        source.volume = volume;
        source.PlayOneShot(clip, volume);

    }

    private void PlayingBackgorundM()
    {
        if (!source2.isPlaying)
        {
            source2.clip = backgroundMusic1;
            source2.Play();
        }
        source2.clip = backgroundMusic1;
        source2.Play();
    }

    public void MenuSFX(AudioClip clip, float volume = 1)
    {
        source3.clip = clip;
        source3.volume = volume;
        source3.PlayOneShot(clip);
    }
    IEnumerator FadeOut(float startVolume = 0.5f, float fadeOutSPeed = 0.0005f)
    {
        fadingIn = false;
        fadingOut = true;
        while (startVolume > 0)
        {
            Debug.Log("While FadeOut");
            startVolume -= fadeOutSPeed;
            source2.volume = startVolume;

            yield return null;

        }

    }

    IEnumerator FadeIn(float targetvolume = 0.5f, float currentVolume = 0f, float fadeInSpeed = 0)
    {
        fadingIn = true;
        fadingOut = false;
        while (currentVolume < targetvolume)
        {
            currentVolume += fadeInSpeed;
            source2.volume = currentVolume;

            backgroundMusicVolume = currentVolume;

            yield return null;
        }

    }


}
