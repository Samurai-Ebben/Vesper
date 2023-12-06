using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SizeChangeAnimation : MonoBehaviour
{
    PlayerController playerController;
    SizeStats sizeStats;

    public float timeForScaling = 5;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        sizeStats = GetComponent<SizeStats>();
    }

    void Update()
    {
            if (playerController.currentSize == Sizes.SMALL)
            {
                ChangingAnimation("small");
            }

            if (playerController.currentSize == Sizes.LARGE)
            {
                ChangingAnimation("large");
            }

            if (playerController.currentSize == Sizes.MEDIUM)
            {
                ChangingAnimation("medium");
            }
    }

    void ChangingAnimation(string playerName)
    {
        float playerSize = 0;

        switch (playerName)
        { 
            case "small":
                playerSize = sizeStats.sizeSmall;
            break;

            case "large":
                playerSize = sizeStats.sizeLarge;
            break;

            case "medium":
                playerSize = sizeStats.sizeMedium;
            break;
        }

        transform.DOScale(playerSize, timeForScaling).SetEase(Ease.OutElastic);
    }
    private void OnDestroy()
    {
        DOTween.Clear(transform);
    }
}

