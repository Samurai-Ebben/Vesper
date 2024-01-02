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
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.landingSmall, AudioManager.Instance.landingVolume);
        }
        else if (playerController.currentSize == Sizes.MEDIUM)
        {
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.landingMedium, AudioManager.Instance.landingVolume);
        }
        else if (playerController.currentSize == Sizes.LARGE)
        {
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.landingBig, AudioManager.Instance.landingVolume);
        }
    }

    public void PlayJumpingSound()
    {
        if (AudioManager.Instance == null) return;
        if (playerController.currentSize == Sizes.SMALL)
        {
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.jumpSmall, AudioManager.Instance.jumpingVolume);
        }
        else if (playerController.currentSize == Sizes.MEDIUM)
        {
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.jumpMedium, AudioManager.Instance.jumpingVolume);
        }
        else if (playerController.currentSize == Sizes.LARGE)
        {
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.jumpBig, AudioManager.Instance.jumpingVolume);
        }
    }

    public void PlaySwitchToLarge()
    {
        if (AudioManager.Instance == null) return;
        AudioManager.Instance.GameplaySFX(AudioManager.Instance.switchToLarge, AudioManager.Instance.switchVolume);
    }

    public void PlaySwitchToSmall()
    {
        if (AudioManager.Instance == null) return;
        AudioManager.Instance.GameplaySFX(AudioManager.Instance.switchToSmall, AudioManager.Instance.switchVolume);
    }


}
