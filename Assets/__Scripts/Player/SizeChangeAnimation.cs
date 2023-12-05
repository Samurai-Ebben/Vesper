using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GameState
{
    GettingBig,
    GettingSmall
}
public class SizeChangeAnimation : MonoBehaviour
{

    GameState state;
    PlayerController playerController;
    AnimationClip gettingSmall;
    RayCastHandler rayCastHandler;
    SizeStats sizeStats;

    public float timeForScaling = 5;

    public bool smallCharacterAnimation;
    public bool mediumCharacterAnimation;
    public bool largeCharacterAnimation;



    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rayCastHandler = GetComponent<RayCastHandler>();
        sizeStats = GetComponent<SizeStats>();
    }

    // Update is called once per frame
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

