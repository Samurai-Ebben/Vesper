using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class RayCastHandler : MonoBehaviour
{
    BoxCollider2D col;
    PlayerController controller;

    [Header("RayCast Length")]
    public float smallRaycastLength = 0.25f;
    public float mediumRaycastLength = 0.50f;
    public float largeRaycastLength = 0.75f;

    public float helperCheckLength = 0.5f;

    [Header("Total Raycast")]
    public int totalTopRaycast = 3;
    public int totalSideRaycast = 3;
    public int totalDownRaycast = 3;
    int totalDiagonalRaycast = 1;

    [Header("Offset")]
    public float helpOffset = 0.5f;
    public float edgeOffset = 1.5f;
    float offset;




    public float sideCheckLength;
    public float diagonalLength = 0.5f;

    //since this is ground, maybe rename it?
    public LayerMask mask;


    RaycastHit2D centerRaycast;
    RaycastHit2D helperRaycast;

    public float drawRayForMedium;
    public float drawRayForLarge;
    public float drawRayForSmall;

    public bool mediumTopIsFree;
    public bool largeTopIsFree;
    public bool smallTopIsFree;

    public bool rightSide;
    public bool leftSide;
    public bool anySide;

    public bool rightTop;
    public bool rightDown;
    public bool leftDown;
    public bool leftTop;
    public bool diagonalTop;

    public bool smallDownIsFree;
    public bool mediumDownIsFree;
    public bool largeDownIsFree;

    public bool rightHelpCheck;
    public bool leftHelpCheck;

    public bool helpingPush;





    public bool diagonalCheck;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        controller = GetComponent<PlayerController>();
        col = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        smallTopIsFree = RayCastGenerator(smallRaycastLength, Vector2.up, totalTopRaycast);
        largeTopIsFree = RayCastGenerator(largeRaycastLength, Vector2.up, totalTopRaycast);
        mediumTopIsFree = RayCastGenerator(mediumRaycastLength, Vector2.up, totalTopRaycast);


        rightSide = RayCastGenerator(sideCheckLength, Vector2.right, totalSideRaycast);
        leftSide = RayCastGenerator(sideCheckLength, Vector2.left, totalSideRaycast);
        anySide = leftSide || rightSide;

        smallDownIsFree = RayCastGenerator(smallRaycastLength, Vector2.down, totalDownRaycast);
        largeDownIsFree = RayCastGenerator(largeRaycastLength, Vector2.down, totalDownRaycast);
        mediumDownIsFree = RayCastGenerator(mediumRaycastLength, Vector2.down, totalDownRaycast);

        rightHelpCheck = RayCastForHelper();

        rightTop = RayCastGenerator(diagonalLength, new Vector2(1, 1), totalDiagonalRaycast);
        rightDown = RayCastGenerator(diagonalLength, new Vector2(1, -1), totalDiagonalRaycast);
        leftDown = RayCastGenerator(diagonalLength, new Vector2(-1, -1), totalDiagonalRaycast);
        leftTop = RayCastGenerator(diagonalLength, new Vector2(-1, 1), totalDiagonalRaycast);
        diagonalTop = rightTop || leftTop;

        if (!smallTopIsFree)
        {
            helpingPush = true;
        }
        else
        {
            helpingPush = false;
        }

    }
    bool RayCastGenerator(float characterSize, Vector2 direction, int totalRaycast)
    {

        

        Vector3 center = new Vector3(transform.position.x, transform.position.y);

        bool canChangeSize = true;

        for (int i = 0; i < totalRaycast; i++)
        {
            if (direction == Vector2.up || direction == Vector2.down)
            {
                offset = (col.bounds.size.x * edgeOffset) / totalRaycast;
                centerRaycast = Physics2D.Raycast(center + new Vector3((i - (totalRaycast - 1) / 2f) * offset, 0), direction, characterSize, mask);
                Debug.DrawRay(center + new Vector3((i - (totalRaycast - 1) / 2f) * offset, 0), direction * characterSize, Color.red);
            }

            else if (direction == Vector2.right || direction == Vector2.left)
            {
                offset = (col.bounds.size.y * edgeOffset) / totalRaycast;
                centerRaycast = Physics2D.Raycast(center + new Vector3(0, (i - (totalRaycast - 1) / 2f) * offset), direction, characterSize, mask);
                Debug.DrawRay(center + new Vector3(0, (i - (totalRaycast - 1) / 2f) * offset), direction * characterSize, Color.red);
            }
            else if (direction == new Vector2(1, 1) || direction == new Vector2(1, -1) || direction == new Vector2(-1, 1) || direction == new Vector2(-1, -1))
            {
                offset = (col.bounds.size.y * edgeOffset) / totalRaycast;
                centerRaycast = Physics2D.Raycast(center + new Vector3((i - (totalRaycast - 1) / 2f), (i - (totalRaycast - 1) / 2f) * offset), direction, characterSize, mask);
                Debug.DrawRay(center + new Vector3((i - (totalRaycast - 1) / 2f), (i - (totalRaycast - 1) / 2f) * offset), direction * characterSize, Color.red);
            }


            canChangeSize = centerRaycast.collider == null;

            if (centerRaycast.collider != null)
            {
                canChangeSize = false;
                break;
            }

        }
        return canChangeSize;



    }

    bool RayCastForHelper()
    {
        bool rightCheck = false;
        bool leftCheck = false;
        bool centerCheck = false;


        //float leftOffset = transform.position.x - drawRay;

        Vector3 center = col.bounds.size;
        Vector3 right = col.bounds.size;
        Vector3 left = col.bounds.size;

        center.x = 0;
        right.x -= -helpOffset;
        left.x -= helpOffset;


        rightCheck = Physics2D.Raycast(right, Vector2.up);
        Debug.DrawRay(right, Vector2.up * helperCheckLength, Color.black);

        leftCheck = Physics2D.Raycast(left, Vector2.up);
        Debug.DrawRay(left, Vector2.up * helperCheckLength, Color.black);

        centerCheck = Physics2D.Raycast(center, Vector2.up);
        Debug.DrawRay(center, Vector2.up * helperCheckLength, Color.black);




        return helperRaycast;


    }

    //private void DirectionHandler(float characterSize, Vector2 direction, int totalRaycast, Vector3 center, int i)
    //{
    //    Vector3 offsetBetweenRay = Vector2.zero;

    //    if (direction == Vector2.up || direction == Vector2.down)
    //    {
    //        offset = (col.bounds.size.x * edgeOffset) / totalRaycast;
    //        offsetBetweenRay = new Vector2((i - (totalRaycast - 1) / 2f) * offset, 0);
    //    }
    //    else if (direction == Vector2.right || direction == Vector2.left)
    //    {
    //        offset = (col.bounds.size.y * edgeOffset) / totalRaycast;
    //        offsetBetweenRay = new Vector2(0, ((i - totalRaycast - 1) / 2f) * offset);
    //    }


    //    centerRaycast = Physics2D.Raycast(center + offsetBetweenRay, direction, characterSize, mask);
    //    Debug.DrawRay(center + offsetBetweenRay, direction * characterSize, Color.red);
    //}
}
