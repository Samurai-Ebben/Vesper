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

    public float timeForScaling = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rayCastHandler = GetComponent<RayCastHandler>();
    }

    // Update is called once per frame
    void Update()
    {
               
        if(rayCastHandler.canChangeSize == true) 
        {
            if (playerController.isSmall)
            {
                transform.DOScale(0.25f, timeForScaling).SetEase(Ease.OutElastic);
                // do smaller animation
            }
            if (playerController.isLarge)
            {
                transform.DOScale(1.25f, timeForScaling).SetEase(Ease.OutElastic);
                // do smaller animation
            }
            if (!playerController.isSmall && !playerController.isLarge)
            {
                transform.DOScale(1, timeForScaling).SetEase(Ease.OutElastic);
            }
        }
        
    }

    
}
