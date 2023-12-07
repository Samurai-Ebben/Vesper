using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour
{
    [Header("Duration")]
    public float destructionDuration = 0.04f;
    public float jumpDuration = 0.07f;

    [Header("Strength")]
    public float strengthForDestruction = 0.1f;
    public float strengthForJump = 0.01f;

    public bool vertical;
    public bool horizontal;

    Vector3 origPos;

    Vector3 RandomPosition;
    
    void Start()
    {
        origPos = transform.position;
    }

    void Update()
    {        
        
        
    }

    public void JumpShake()
    {
            StartCoroutine(ShakeScreen(strengthForJump, jumpDuration));
    }

    public void DestructionShake()
    {
        StartCoroutine(ShakeScreen(strengthForDestruction, destructionDuration));
    }

    IEnumerator ShakeScreen(float strength, float duration)
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
