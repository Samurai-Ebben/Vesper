using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GameState
{
    GettingBig,
    GettingSmall
}
public class AnimationHandler : MonoBehaviour
{

    GameState state;
    PlayerController playerController;
    AnimationClip gettingSmall;
    RayCastHandler rayCastHandler;
    SizeStats sizeStats;

    public float timeForScaling = 5;
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
                transform.DOScale(sizeStats.sizeSmall, timeForScaling).SetEase(Ease.OutElastic);
                // do smaller animation
            }
            if (playerController.currentSize == Sizes.MEDIUM)
            {
                transform.DOScale(sizeStats.sizeMedium, timeForScaling).SetEase(Ease.OutElastic);
            }
            if (playerController.currentSize == Sizes.LARGE)
            {
                transform.DOScale(sizeStats.sizeLarge, timeForScaling).SetEase(Ease.OutElastic);
                // do smaller animation
            }
        
        
    }

    
}
