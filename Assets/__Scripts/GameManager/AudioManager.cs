using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    PlayerController player;

    public bool startLandingSound;

    public float volume = 2;
    public float landingSound = 2;

    public AudioClip jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        player = PlayerController.instance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayingAudio(AudioClip clip, float volume)
    {
       source.clip = clip;
       source.volume = volume;
       source.PlayOneShot(clip, volume);
    }
}
