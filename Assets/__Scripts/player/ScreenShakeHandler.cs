using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour
{
    public float duration = 2;
    public float strength = 1.0f;

    Vector3 origPos;
    public bool startTheShake;
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
     
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            StartCoroutine(ShakeScreen()); 
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
