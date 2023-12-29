using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    PlayerController playerController;
    AudioManager audioManager;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        playerController = PlayerController.instance;
        //audioManager = FindAnyObjectByType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayLandingSound()
    {
        if (AudioManager.Instance == null)
        {
            return;
        }
        if (playerController.currentSize == Sizes.SMALL)
        {
            AudioManager.Instance.PlayingAudio(AudioManager.Instance.landingSmall);
        }
        else if (playerController.currentSize == Sizes.MEDIUM)
        {
            AudioManager.Instance.PlayingAudio(AudioManager.Instance.landingMedium);
        }
        else if (playerController.currentSize == Sizes.LARGE)
        {
            AudioManager.Instance.PlayingAudio(AudioManager.Instance.landingBig);
        }
    }

    public void PlayJumpingSound()
    {
        if (AudioManager.Instance == null) return;
        if (playerController.currentSize == Sizes.SMALL)
        {
            AudioManager.Instance.PlayingAudio(AudioManager.Instance.jumpSmall);
        }
        else if (playerController.currentSize == Sizes.MEDIUM)
        {
            AudioManager.Instance.PlayingAudio(AudioManager.Instance.jumpMedium);
        }
        else if (playerController.currentSize == Sizes.LARGE)
        {
            AudioManager.Instance.PlayingAudio(AudioManager.Instance.jumpBig);
        }
    }

    public void PlaySwitchToLarge()
    {
        if (AudioManager.Instance == null) return;
        AudioManager.Instance.PlayingAudio(AudioManager.Instance.switchToLarge);
    }

    public void PlaySwitchToSmall()
    {
        if (AudioManager.Instance == null) return;
        AudioManager.Instance.PlayingAudio(AudioManager.Instance.switchToSmall);
    }


}
