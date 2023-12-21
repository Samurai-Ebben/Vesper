using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    public AudioSource source2;
    PlayerController player;

    public bool startLandingSound;

    public float volume = 2;
    public float landingSound = 2;
    public float jumpingVolume = 2;
    public float deathVolume = 2;
    public float backgroundMusicVolume = 2;


    public AudioClip jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium, death, backgroundMusic;
    public List<AudioClip> clips;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        player = PlayerController.instance;
        clips = new List<AudioClip> { jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium };

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
