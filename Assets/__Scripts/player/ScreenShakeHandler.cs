using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour
{
    PlayerController controller;
    public float duration = 2;
    public float strength = 1.0f;

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
        //if(controller.isFalling)
        //{
        //    StartCoroutine(ShakeScreen());
        //    Debug.Log("shake");
        //}
        //if(!controller.isFalling ) 
        //{
        //    StopCoroutine(ShakeScreen());
        //}

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
