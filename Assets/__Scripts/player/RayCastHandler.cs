using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{
    PlayerController controller;
    public float checkingTheTop = 0.25f;
    float smallPlayerRay = 0.25f;
    float mediumPlayerRay = 0.50f;
    float largePlayerRay = 0.75f;

    public float sideCheckLength;
    public float diagonalLength = 0.5f;

    public LayerMask isBlock;

    float drawRay = 1;

    public float drawRayForMedium;
    public float drawRayForLarge;
    public float drawRayForSmall;

    public bool mediumCanChangeSize;
    public bool largeCanChangeSize;
    public bool smallCanChangeSize;

    public bool sideCheck;
    public bool diagonalCheck;   

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
            smallCanChangeSize = RayCastGenerator(smallPlayerRay, Vector2.up, Color.red);
        
            largeCanChangeSize = RayCastGenerator(largePlayerRay, Vector2.up, Color.red);
        
            mediumCanChangeSize = RayCastGenerator(mediumPlayerRay, Vector2.up, Color.red);


            sideCheck = RayCastGenerator(sideCheckLength, Vector2.right, Color.green) || RayCastGenerator(sideCheckLength, Vector2.left, Color.green);
            diagonalCheck = RayCastGenerator(diagonalLength, new Vector2(-1, 1), Color.green) || RayCastGenerator(diagonalLength, new Vector2(1, 1), Color.red);



        bool RayCastGenerator(float characterSize, Vector2 direction, Color rayColor)
    {
        bool canChangeSize;
        if(characterSize == mediumPlayerRay)
        {
            drawRay = drawRayForMedium;
        }

            if(characterSize == smallPlayerRay) 
            {
                drawRay = drawRayForSmall;
            }

            if(characterSize == largePlayerRay)
            {
                drawRay = drawRayForLarge;   
            }
        
                float leftOffset = transform.position.x - drawRay;
                Vector3 left = new Vector3(leftOffset, transform.position.y);
        
        
                float rightOffset = transform.position.x + drawRay;
                Vector3 right = new Vector3(rightOffset, transform.position.y);
        


        RaycastHit2D leftTopRay = Physics2D.Raycast(left, direction, characterSize, isBlock);
        RaycastHit2D rightTopRay = Physics2D.Raycast(right, direction, characterSize, isBlock);


            // for debug and colors
            if (rayColor != Color.red)
        {
            Debug.DrawRay(left, direction * characterSize, rayColor);
            Debug.DrawRay(right, direction * characterSize, rayColor);

            }
        else
        {
            Debug.DrawRay(left, direction * characterSize, Color.red);
            Debug.DrawRay(right, direction * characterSize, Color.red);

            }

            canChangeSize = leftTopRay.collider == null && rightTopRay.collider == null;
            //Debug.Log(canChangeSize);
        return canChangeSize;
    }

    }

}
