using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezeFollow : MonoBehaviour
{
    public Squeeze squeeze;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (squeeze.isSqueezing)
        {
            transform.position = originalPosition + transform.up * -squeeze.deltaY;
        }

        if (squeeze.isReturning)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, squeeze.returnSpeed * Time.deltaTime);
        }
    }
}
