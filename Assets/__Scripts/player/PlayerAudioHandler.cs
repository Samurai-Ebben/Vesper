using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    PlayerController playerController;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.instance;
        audioManager = FindAnyObjectByType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(audioManager);
    }

    public void PlayLandingSound()
    {
        if (playerController.currentSize == Sizes.SMALL)
        {
            audioManager.PlayingAudio(audioManager.landingSmall, audioManager.landingSound);
        }
        else if (playerController.currentSize == Sizes.MEDIUM)
        {
            audioManager.PlayingAudio(audioManager.landingMedium, audioManager.landingSound);
        }
        else if (playerController.currentSize == Sizes.LARGE)
        {
            audioManager.PlayingAudio(audioManager.landingBig, audioManager.landingSound);
        }
    }

    public void PlayJumpingSound()
    {
        if (playerController.currentSize == Sizes.SMALL)
        {
            audioManager.PlayingAudio(audioManager.jumpSmall, audioManager.jumpingVolume);
        }
        else if (playerController.currentSize == Sizes.MEDIUM)
        {
            audioManager.PlayingAudio(audioManager.jumpMedium, audioManager.jumpingVolume);
        }
        else if (playerController.currentSize == Sizes.LARGE)
        {
            audioManager.PlayingAudio(audioManager.jumpBig, audioManager.jumpingVolume);
        }
    }

}
