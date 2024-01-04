using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 endPosition;

    public Vector3 cameraStartPosition;
    public Vector3 cameraEndPosition;

    public float mulitplier = 1.0f;

    void Start()
    {
        // End position is the initial position
        endPosition = transform.position;

        Vector3 cameraDelta = cameraStartPosition - cameraEndPosition;
        Vector3 objectDelta = new Vector3(cameraDelta.x * mulitplier, cameraDelta.y * mulitplier);

        startPosition = endPosition - objectDelta;
        transform.position = startPosition;
    }

    void Update()
    {
        float t = Mathf.InverseLerp(0f, 1f, (Camera.main.transform.position.x - cameraStartPosition.x) / (cameraEndPosition.x - cameraStartPosition.x));

        Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);

        transform.position = newPosition;
    }
}
