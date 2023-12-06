using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour
{
    PlayerController controller;
    public float duration = 0.25f;
    public float strength = 0.5f;

    Vector3 origPos;
    public bool startTheShake;
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
        controller = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.currentSize == Sizes.LARGE) 
        { 
            StartShake();
        }

    }

    private void StartShake()
    {
        if (controller.hasLanded)
        {
            StartCoroutine(ShakeScreen());
        }
        if (!controller.hasLanded)
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
