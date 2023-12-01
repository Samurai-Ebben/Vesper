using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingMovement : MonoBehaviour
{

    public float targetHeight;
    public float riseSpeed = 2.0f;

    private Vector3 initialPosition;
    public bool isRising = false;

    public enum States {DOWN,UP };
    public States currentState;

    public float timer;

    void Start()
    {
        initialPosition = transform.position;
        currentState = States.DOWN;

    }

    void Update()
    {
        timer -= Time.deltaTime;
        switch (currentState)
        {
            case States.DOWN:
                if (timer > 0) return;

                if (transform.position.y > initialPosition.y)
                {
                    float step = riseSpeed * Time.deltaTime;
                    transform.Translate(Vector3.down * step);
                }

                break;
          
            case States.UP:
                if (transform.position.y < targetHeight)
                {
                    float step = riseSpeed * Time.deltaTime;
                    transform.Translate(Vector3.up * step);
                }
                break;
        }


        return;
       
    }

    public void Rise()
    {
        currentState = States.UP;
        return;
        isRising = true;
        //StopCoroutine("DescendCoroutine");
    }

    public void Descend()
    {
        if (currentState == States.DOWN)
        {
            return;
        }
        currentState = States.DOWN;
        timer = 2;


        // isRising = false;
        // StartCoroutine(DescendCoroutine());
    }

    //IEnumerator DescendCoroutine()
    //{
    //    yield return new WaitForSeconds(2);
    //    while (transform.position.y > initialPosition.y)
    //    {
    //        float step = riseSpeed * Time.deltaTime;
    //        transform.Translate(Vector3.down * step);
    //        yield return null;
    //    }
    //}
}
