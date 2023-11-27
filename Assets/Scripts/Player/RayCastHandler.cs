using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{
    public float checkingTheTop = 0.25f;
    float rayCastToTop;
    Collider2D coll;
    public bool canChangeSize;


    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        rayCastToTop = checkingTheTop * coll.bounds.size.y;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayCastToTop);
        if(hit.collider != null)
        {
            Debug.Log("HItted something!");
            canChangeSize = false;

        }
        else
        {
            canChangeSize= true;
        }
        Debug.DrawRay(transform.position, Vector2.up * rayCastToTop, Color.red);
    }
}
