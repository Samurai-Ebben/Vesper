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

    public bool smallCharacter;
    public bool mediumCharacter;
    public bool largeCharacter;

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
               
        
            if (smallCharacter)
            {
                transform.DOScale(sizeStats.sizeSmall, timeForScaling).SetEase(Ease.OutElastic);
                // do smaller animation
            }
            if (mediumCharacter)
            {
                transform.DOScale(sizeStats.sizeMedium, timeForScaling).SetEase(Ease.OutElastic);
            }
            if (largeCharacter)
            {
                transform.DOScale(sizeStats.sizeLarge, timeForScaling).SetEase(Ease.OutElastic);
                // do smaller animation
            }
        
        
    }

    
}
