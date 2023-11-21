using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSize : MonoBehaviour
{
    public GameObject smallSize;
    public GameObject mediumSize;
    public GameObject bigSize;




    GameObject currentGameObject;

    public bool isBig;
    public bool isSmall;

    Vector2 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        bigSize.GetComponent<SpriteRenderer>().enabled = false;
        smallSize.GetComponent<SpriteRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;


        
        if(isBig) 
        {
            BigSize();
            isSmall = false;
        }
        else if (isSmall)
        {
            SmallSize();
            isBig = false;

        }
        else
        {
            DisableAll();
        }

        
      






      
        
    }

    void BigSize()
    {
        bigSize.GetComponent<SpriteRenderer>().enabled = true;
        smallSize.GetComponent<SpriteRenderer>().enabled = false;
        mediumSize.GetComponent<SpriteRenderer>().enabled = false;
    }

    void SmallSize()
    {
        bigSize.GetComponent<SpriteRenderer>().enabled = false;
        smallSize.GetComponent<SpriteRenderer>().enabled = true;
        mediumSize.GetComponent<SpriteRenderer>().enabled= false;
    }

    void DisableAll()
    {
        bigSize.GetComponent<SpriteRenderer>().enabled = false;
        smallSize.GetComponent<SpriteRenderer>().enabled = false;
        mediumSize.GetComponent<SpriteRenderer>().enabled = true;

    }




}
