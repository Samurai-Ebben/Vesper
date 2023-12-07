using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour
{
    PlayerController playerController;
    public float duration = 0.25f;
    public float strength = 0.5f;

    public bool vertical;
    public bool horizontal;

    Vector3 origPos;
    public bool startTheShake;

    Vector3 RandomPosition;
    
    void Start()
    {
        origPos = transform.position;
        playerController = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        
        if(playerController.currentSize == Sizes.LARGE) 
        { 
            StartShake();
        }
    }

    private void StartShake()
    {
        if (playerController.hasLanded)
        {
            StartCoroutine(ShakeScreen());
        }
        if (!playerController.hasLanded)
        {
            StopCoroutine(ShakeScreen());
        }
    }

    IEnumerator ShakeScreen()
    {
        if (vertical)
        {
            horizontal = false;
            RandomPosition.y = Random.Range(0, 10);
        }

        if (horizontal)
        {
            vertical = false;
            RandomPosition.x = Random.Range(0, 10);
        }

        if(!horizontal && !vertical) 
        {
            RandomPosition = Random.insideUnitCircle;
        }
        float timeElapsed = 0;
        
        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;  

            transform.position = transform.position + RandomPosition * strength;
            yield return null;
        }

        transform.position = origPos;   


    }
}
