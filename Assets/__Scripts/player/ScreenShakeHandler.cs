using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour
{
    PlayerController playerController;
    public float duration = 0.25f;
    public float strength = 0.5f;

    Vector3 origPos;
    public bool startTheShake;
    
    void Start()
    {
        origPos = transform.position;
        playerController = PlayerController.instance;
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
        float timeElapsed = 0;
        

        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;  

            transform.position = transform.position + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = origPos;   


    }
}
