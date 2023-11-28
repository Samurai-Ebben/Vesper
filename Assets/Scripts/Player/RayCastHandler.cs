using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{
    PlayerController controller;
    public float checkingTheTop = 0.25f;
    float smallRay = 0.25f;
    float mediumRay = 0.50f;
    float largeRay = 0.75f;

    public float drawRay = 1;

    Collider2D coll;
    public bool canChangeSize;


    
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

        float rightOffset = transform.position.x + drawRay;
        Vector3 right = new Vector3(rightOffset, transform.position.y);

        float leftOffset = transform.position.x - drawRay;
        Vector3 left = new Vector3(leftOffset, transform.position.y); 

        //RaycastHit2D leftSmall = Physics2D.Raycast(left, Vector2.up, smallRay);
        //RaycastHit2D leftLarge = Physics2D.Raycast(left, Vector2.up, largeRay);

        //RaycastHit2D rightSmall = Physics2D.Raycast(right, Vector2.up, smallRay);
        //RaycastHit2D rightMedium = Physics2D.Raycast(right, Vector2.up, mediumRay);
        //RaycastHit2D rightLarge = Physics2D.Raycast(right, Vector2.up, largeRay);




        //Debug.DrawRay(right, Vector2.up * smallRay, Color.red);
        //Debug.DrawRay(right, Vector2.up * mediumRay, Color.yellow);
        //Debug.DrawRay(right, Vector2.up * largeRay, Color.green);

        //Debug.DrawRay(left, Vector2.up * smallRay, Color.red);
        //Debug.DrawRay(left, Vector2.up * mediumRay, Color.yellow);
        //Debug.DrawRay(left, Vector2.up * largeRay, Color.green);

        RayCastGenerator(left, largeRay, Color.red);



        if(controller.isSmall)
        {

        }

            canChangeSize = true;

        //if (hit.collider != null)
        //{

        //}
        //else
        //{
        //    canChangeSize= true;
        //}
    }

    void RayCastGenerator(Vector3 sendingRaycastFrom, float characterSize, Color rayColor)
    {
        Physics2D.Raycast(sendingRaycastFrom, Vector2.up, characterSize);
        if(rayColor != Color.red)
        {
            Debug.DrawRay(sendingRaycastFrom, Vector2.up * characterSize, rayColor);
        }
        else
        {
            Debug.DrawRay(sendingRaycastFrom, Vector2.up * characterSize, Color.red);
        }
    }
}
