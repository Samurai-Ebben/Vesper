using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    public AudioSource source2;
    public AudioSource source3;
    public static AudioManager Instance { get; private set; }

    public bool startLandingSound;


    public AudioClip jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium, death, backgroundMusic, collectible, pauseMenu;
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
        PlayingBackgorundM();
    }

    

    private void Update()
    {
    }
    public void PlayingAudio(AudioClip clip)
    {
       source.clip = clip;
        source.PlayOneShot(clip, 1);

    }

    private void PlayingBackgorundM()
    {
        source2.clip = backgroundMusic;
        source2.PlayOneShot(backgroundMusic, 1);
    }

    public void MenuSFX(AudioClip clip)
    {
        source3.clip = clip;
        source3.PlayOneShot(clip);
    }
}
