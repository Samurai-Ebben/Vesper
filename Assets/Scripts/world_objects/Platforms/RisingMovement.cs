using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingMovement : MonoBehaviour
{
    public List<Transform> coordinates;

    public float targetHeight;
    public float riseSpeed = 2.0f;

    private Vector3 initialPosition;
    public bool isRising = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (isRising && transform.position.y < targetHeight)
        {
            float step = riseSpeed * Time.deltaTime;
            transform.Translate(Vector3.up * step);
        }
    }

    public void Rise()
    {
        isRising = true;
        //StopCoroutine("DescendCoroutine");
    }

    public void Descend()
    {
        isRising = false;
        StartCoroutine(DescendCoroutine());
    }

    IEnumerator DescendCoroutine()
    {
        //yield return new WaitForSeconds(2);
        while (transform.position.y > initialPosition.y)
        {
            float step = riseSpeed * Time.deltaTime;
            transform.Translate(Vector3.down * step);
            yield return null;
        }
    }
}
