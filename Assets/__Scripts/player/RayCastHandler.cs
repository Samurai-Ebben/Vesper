using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
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
    public float smallSideCheck = 0.1f;
    public float mediumSideCheck = 0.2f;
    public float largeSideCheck = 0.3f;
    public float sideCheckLength;
    public float diagonalLength = 0.5f;
    public float drawRayForMedium;
    public float drawRayForLarge;
    public float drawRayForSmall;
    float checkGroundOffset = 0.50f;
    float checkGorundLength = 0.76f;

    [Header("Total Raycast")]
    public int totalTopRaycast = 3;
    public int totalSideRaycast = 3;
    public int totalDownRaycast = 3;
    int totalDiagonalRaycast = 1;
    public int totalRaycastHelpTop = 3;

    [Header("Offset")]
    public float helpOffset = 0.23f;
    public float edgeOffset = 1.5f;
    float offset;
    public float centerOneOffset = 0.2f;
    public float centerTwoOffset = 0.2f;
    public float centerThreeOffset = 0.2f;
    float sideCheckOffset;

    [Header("Reference")]
    public string playerTag;

    [Header("Bools")]
    //check Top For Size
    public bool mediumTopIsFree;
    public bool largeTopIsFree;
    public bool smallTopIsFree;
    //check sides
    public bool rightSide;
    public bool leftSide;
    public bool anySide;
    //check diagonal
    public bool rightTop;
    public bool rightDown;
    public bool leftDown;
    public bool leftTop;
    public bool diagonalTop;
    //check down
    public bool smallDownIsFree;
    public bool mediumDownIsFree;
    public bool largeDownIsFree;
    public bool checkAllToGround;
    public bool fullyOnPlatform;
    //check for helpers
    public bool rightHelpCheck;
    public bool leftHelpCheck;
    public bool anySideHelpCheck;
    public bool helpingPush;
    public bool centerIsFree;

    [Header("Mask")]
    public LayerMask mask;
    public LayerMask platform;

    //since this is ground, maybe rename it?


    RaycastHit2D centerRaycast;
    RaycastHit2D helperRaycast;

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
        ResizeTheRaycast();




        smallTopIsFree = RayCastGenerator(smallRaycastLength, Vector2.up, totalTopRaycast);
        largeTopIsFree = RayCastGenerator(largeRaycastLength, Vector2.up, totalTopRaycast);
        mediumTopIsFree = RayCastGenerator(mediumRaycastLength, Vector2.up, totalTopRaycast);


        rightSide = RayCastGenerator(sideCheckLength, Vector2.right, totalSideRaycast);
        leftSide = RayCastGenerator(sideCheckLength, Vector2.left, totalSideRaycast);

        anySide = leftSide || rightSide;

        smallDownIsFree = RayCastGenerator(smallRaycastLength, Vector2.down, totalDownRaycast);
        largeDownIsFree = RayCastGenerator(largeRaycastLength, Vector2.down, totalDownRaycast);
        mediumDownIsFree = RayCastGenerator(mediumRaycastLength, Vector2.down, totalDownRaycast);

        rightHelpCheck = RayCastForHelper(helperCheckLength, helpOffset);
        leftHelpCheck = RayCastForHelper(helperCheckLength, -helpOffset);
        anySideHelpCheck = rightHelpCheck || leftHelpCheck;

        centerIsFree = RayCastForHelper(helperCheckLength, centerOneOffset) && RayCastForHelper(helperCheckLength, centerTwoOffset) && RayCastForHelper(helperCheckLength, centerThreeOffset);


        rightTop = RayCastGenerator(diagonalLength, new Vector2(1, 1), totalDiagonalRaycast);
        rightDown = RayCastGenerator(diagonalLength, new Vector2(1, -1), totalDiagonalRaycast);
        leftDown = RayCastGenerator(diagonalLength, new Vector2(-1, -1), totalDiagonalRaycast);
        leftTop = RayCastGenerator(diagonalLength, new Vector2(-1, 1), totalDiagonalRaycast);
        diagonalTop = rightTop || leftTop;

        checkAllToGround = RayCastGenerator(smallRaycastLength, Vector2.down, totalDownRaycast, true);

        DetectGround(playerTag);

    }

    private void DetectGround(string compareGroundToTag)
    {
        Vector3 center = transform.position;
        Vector3 right = transform.position;
        Vector3 left = transform.position;

        right.x -= -checkGroundOffset;
        left.x -= checkGroundOffset;

        RaycastHit2D hitFromRight = Physics2D.Raycast(right, Vector2.down, checkGorundLength);
        RaycastHit2D hitFromLeft = Physics2D.Raycast(left, Vector2.down, checkGorundLength);
        RaycastHit2D hitFromCenter = Physics2D.Raycast(center, Vector2.down, checkGorundLength);

        checkAllToGround = false;

        if (hitFromRight.collider != null && hitFromLeft.collider != null && hitFromCenter.collider != null)
        {
            checkAllToGround = true;
        }



        Debug.DrawRay(right, Vector2.down * checkGorundLength, Color.yellow);
        Debug.DrawRay(left, Vector2.down * checkGorundLength, Color.yellow);
        Debug.DrawRay(center, Vector2.down * checkGorundLength, Color.yellow);




        if (checkAllToGround)
        {
            if (hitFromRight.collider.CompareTag(compareGroundToTag) && hitFromLeft.collider.CompareTag(compareGroundToTag) && hitFromCenter.collider.CompareTag(compareGroundToTag))
            {
                fullyOnPlatform = true;
            }
        }
        else
        {
            fullyOnPlatform = false;
        }
    }

    private void ResizeTheRaycast()
    {
        if (controller.currentSize == Sizes.SMALL)
        {
            helpOffset = 0.11f;
            helperCheckLength = 0.22f;
            sideCheckLength = 0.12f;

            sideCheckLength = smallSideCheck;

        }
        else if (controller.currentSize == Sizes.BIG)
        {
            helpOffset = 0.48f;
            helperCheckLength = 0.88f;
            sideCheckLength = 0.5f;

            sideCheckLength = largeSideCheck;


        }
        else if (controller.currentSize == Sizes.MEDIUM)
        {
            helpOffset = 0.23f;
            helperCheckLength = 0.5f;
            sideCheckLength = 0.25f;

            sideCheckLength = mediumSideCheck;


        }
    }

    bool RayCastGenerator(float characterSize, Vector2 direction, int totalRaycast, bool allOfThem = false)
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




            if (centerRaycast.collider != null)
            {
                if (allOfThem)
                {
                    canChangeSize = false;
                    continue;
                }
                else
                {
                    canChangeSize = false;
                    return canChangeSize;
                }
            }

        }
            return canChangeSize;

    }
    bool RayCastForHelper(float rayLength, float offset)
    {
        bool rightCheck = false;
        bool leftCheck = false;
        bool centerCheck = false;

        Vector2 originPos = new Vector2(transform.position.x + offset, transform.position.y);

        rightCheck = Physics2D.Raycast(new Vector2(originPos.x, originPos.y), Vector2.up, rayLength, mask);
        Debug.DrawRay(new Vector2(originPos.x, originPos.y), Vector2.up * rayLength, Color.black);

        return rightCheck;

    }
}
