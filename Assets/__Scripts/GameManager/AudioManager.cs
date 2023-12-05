using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    PlayerController player;

    public float volume = 2;
    public float landingSound = 2;

    public AudioClip jumpSmall, jumpBig, jumpMedium, landingSmall, landingMedium, landingBig, switchToLarge, switchToSmall, switchToMedium;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();   
        player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.isJumping && player.currentSize == Sizes.SMALL)
        //{
        //    PlayingAudio(jumpSmall, volume);
        //}
        //if (player.isJumping && player.currentSize == Sizes.LARGE)
        //{
        //    PlayingAudio(jumpBig, volume);
        //}
        //if (player.isJumping && player.currentSize == Sizes.MEDIUM)
        //{
        //    PlayingAudio(jumpMedium, volume);
        //}



        if (player.hasLanded && player.currentSize == Sizes.SMALL)
        {
            PlayingAudio(landingSmall, landingSound);
        }
        if (player.hasLanded && player.currentSize == Sizes.MEDIUM)
        {
            PlayingAudio(landingMedium, landingSound);
        }
        if (player.hasLanded && player.currentSize == Sizes.LARGE)
        {
            PlayingAudio(landingBig, landingSound);
        }
    }

    void PlayingAudio(AudioClip clip, float volume)
    {
       source.clip = clip;
       source.volume = volume;
        source.PlayOneShot(clip, volume);
        Debug.Log("Ljud");
    }
}
