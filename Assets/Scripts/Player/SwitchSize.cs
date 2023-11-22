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
        
    }

    // Update is called once per frame
    void Update()
    {


        
        if(isBig) 
        {
            isSmall = false;
            BigSize();
        }
        else if (isSmall)
        {
            isBig = false;
            SmallSize();

        }
        else
        {
            DisableAll();
        }





        

    }

    //void ActivateTargetObj(GameObject targetToActivate, GameObject obj2, GameObject obj3)
    //{
    //    targetToActivate.GetComponent<SpriteRenderer>().enabled = true;
    //    obj2.GetComponent<SpriteRenderer>().enabled = false;
    //    obj3.GetComponent<SpriteRenderer>().enabled = false;

    //    currentPos = targetToActivate.transform.position;

    //    obj2.transform.position = currentPos;
    //    obj3.transform.position = currentPos;
    //}

    void BigSize()
    {
        //ActivateTargetObj(bigSize, smallSize, mediumSize);

        bigSize.GetComponent<SpriteRenderer>().enabled = true;
        smallSize.GetComponent<SpriteRenderer>().enabled = false;
        mediumSize.GetComponent<SpriteRenderer>().enabled = false;

        bigSize.GetComponent<PlayerController>().activeMovementScript = true;
        smallSize.GetComponent<PlayerController>().activeMovementScript = false;
        mediumSize.GetComponent<PlayerController>().activeMovementScript = false;


        currentPos = bigSize.transform.position;

        smallSize.transform.position = currentPos;
        mediumSize.transform.position = currentPos;
    }

    void SmallSize()
    {
        //ActivateTargetObj(smallSize, bigSize, mediumSize);

        bigSize.GetComponent<SpriteRenderer>().enabled = false;
        smallSize.GetComponent<SpriteRenderer>().enabled = true;
        mediumSize.GetComponent<SpriteRenderer>().enabled= false;

        bigSize.GetComponent<PlayerController>().activeMovementScript = false;
        smallSize.GetComponent<PlayerController>().activeMovementScript = true;
        mediumSize.GetComponent<PlayerController>().activeMovementScript = false;

        currentPos = smallSize.transform.position;

        bigSize.transform.position = currentPos;
        mediumSize.transform.position= currentPos;
    }

    void DisableAll()
    {
        bigSize.GetComponent<SpriteRenderer>().enabled = false;
        smallSize.GetComponent<SpriteRenderer>().enabled = false;
        mediumSize.GetComponent<SpriteRenderer>().enabled = true;

        bigSize.GetComponent<PlayerController>().activeMovementScript = false;
        smallSize.GetComponent<PlayerController>().activeMovementScript = false;
        mediumSize.GetComponent<PlayerController>().activeMovementScript = true;

        currentPos = mediumSize.transform.position;

        bigSize.transform.position = currentPos;
        smallSize.transform.position = currentPos;

    }




}
