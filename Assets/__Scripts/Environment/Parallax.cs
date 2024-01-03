using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Vector2 parallaxMult;
    Transform cam;
    Transform origPos;
    Vector3 lastCamPos;
    Transform player;


    void Start()
    {
        //origPos.position = transform.position;
        cam = PlayerController.player.transform;
        lastCamPos = cam.position;

        //Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        Vector3 delta = cam.position - lastCamPos;
        
        //if (GameManager.Instance.IsDead)
        //    cam = Camera.main.transform;
        //else cam = PlayerController.player.transform;

        transform.position += new Vector3(delta.x * parallaxMult.x, delta.y * parallaxMult.y);
        lastCamPos = cam.position;
    }
}
