using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 parallaxMult;
    Transform player;
    Vector3 lastPlayerPos;

    public bool useOrderInLayer;

    Camera mainCamera; 
    bool variablesSet;
    Vector3 endPos;

    void Start()
    {
        mainCamera = Camera.main;
        variablesSet = false;

        player = PlayerController.player.transform;
        lastPlayerPos = player.position;
    }

    void Update()
    {
        if (PlayerIsInCameraView() && !variablesSet)
        {
            SetVariables();
        }

        if (!variablesSet) return;

        Vector3 delta = player.position - lastPlayerPos;

        //if (GameManager.Instance.IsDead)
        //    cam = Camera.main.transform;
        //else cam = PlayerController.player.transform;

        transform.position += new Vector3(delta.x * parallaxMult.x, delta.y * parallaxMult.y);
        lastPlayerPos = player.position;
    }

    void SetVariables()
    {
        // Setting parallaxMult

        //if (useOrderInLayer)
        //{
        //    int orderInLayer = GetComponent<SpriteRenderer>().sortingOrder;
        //    if (orderInLayer == 0)
        //    {
        //        parallaxMult = vector;
        //        return;
        //    }

        //    parallaxMult = 1 / orderInLayer;
        //}


        // Setting relative Position
        transform.position += (endPos - player.position) * 1;

        variablesSet = true;
    }

    private bool PlayerIsInCameraView()
    {
        if (mainCamera == null || player == null)
        {
            Debug.LogError("Camera or player is missing");
            return false;
        }

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(player.position);

        if (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
