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
            // do smaller animation
        }   
    }

    void SmallerPlayer()
    {
        if(Input.GetKey(KeyCode.G)) 
        { 
            state = GameState.GettingBig;
            Debug.Log(state);
            transform.DOScale(2, 2).SetEase(Ease.OutElastic);
            
        }
    }
}
