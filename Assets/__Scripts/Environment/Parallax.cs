using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Vector2 parallaxMult;
    Transform cam;
    Vector3 lastCamPos;
    Transform player;

    void Start()
    {
        cam = PlayerController.player.transform;
        lastCamPos = cam.position;

        //Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        Vector3 delta = cam.position - lastCamPos;

        transform.position += new Vector3(delta.x * parallaxMult.x, delta.y * parallaxMult.y);
        lastCamPos = cam.position;
    }
}
