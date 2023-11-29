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

    float drawRay = 1;

    public float drawRayForMedium;
    public float drawRayForLarge;
    public float drawRayForSmall;

    bool mediumCanChangeSize;
    bool largeCanChangeSize;
    bool smallCanChangeSize;

    Collider2D coll;
    public bool canChangeSize;

    bool hits;


    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        Physics2D.queriesStartInColliders = false;
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {


        if (controller.currentSize == PlayerController.Sizes.SMALL)
        {
        mediumCanChangeSize = RayCastGenerator(mediumPlayerRay, Color.red) || RayCastGenerator(smallPlayerRay, Color.yellow);
    
        }
        if (controller.currentSize == PlayerController.Sizes.LARGE)
        {
            mediumCanChangeSize = RayCastGenerator(mediumPlayerRay, Color.red) || RayCastGenerator(smallPlayerRay, Color.yellow);

        }
        if (controller.currentSize == PlayerController.Sizes.MEDIUM)
        {
            mediumCanChangeSize = RayCastGenerator(mediumPlayerRay, Color.red) || RayCastGenerator(smallPlayerRay, Color.yellow);

        }
        //Debug.Log(hits);

        // //if(hits)
        // //{
        // //       Debug.Log("We hit something");
        // //}
        // //   else
        // //   {
        // //       Debug.Log("Nope");
        // //   }


       

        

        canChangeSize = true;

        //if (hit.collider != null)
        //{

        //}
        //else
        //{
        //    canChangeSize= true;
        //}
    }

    bool RayCastGenerator(float characterSize, Color rayColor)
    {
        bool itHittedSomething;
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
        


        RaycastHit2D leftRay = Physics2D.Raycast(left, Vector2.up, characterSize);
        RaycastHit2D rightRay = Physics2D.Raycast(right, Vector2.up, characterSize);



        // for debug and colors
        if (rayColor != Color.red)
        {
            Debug.DrawRay(left, Vector2.up * characterSize, rayColor);
            Debug.DrawRay(right, Vector2.up * characterSize, rayColor);

        }
        else
        {
            Debug.DrawRay(left, Vector2.up * characterSize, Color.red);
            Debug.DrawRay(right, Vector2.up * characterSize, Color.red);

        }

        if (leftRay.collider != null || rightRay.collider != null)
        {
            itHittedSomething = true;
            return itHittedSomething == true;
        }
        else
            return itHittedSomething = false;
    }


}
