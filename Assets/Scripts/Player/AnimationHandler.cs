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

    public float timeForScaling = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.GettingBig:
                break;

            case GameState.GettingSmall:
                break;
               
        }
        
        SmallerPlayer();

        if(playerController.isSmall) 
        {
            transform.DOScale(0.25f, 4).SetEase(Ease.OutElastic);
            // do smaller animation
        }
        if (playerController.isLarge)
        {
            transform.DOScale(1.25f, 4).SetEase(Ease.OutElastic);
            // do smaller animation
        }
        if(!playerController.isSmall && !playerController.isLarge) 
        {
            transform.DOScale(1, 4).SetEase(Ease.OutElastic);
        }
    }

    void SmallerPlayer()
    {
        if(Input.GetKey(KeyCode.G)) 
        { 
            state = GameState.GettingBig;
            
            
        }
    }
}
