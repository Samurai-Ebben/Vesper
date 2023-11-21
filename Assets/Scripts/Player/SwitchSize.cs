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
    public bool isMedium;

    Vector2 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
        Debug.Log(currentPos);

        if (isBig) 
        {
            bigSize.gameObject.SetActive(true);
            smallSize.gameObject.SetActive(false);
            mediumSize.gameObject.SetActive(false);
            
        }
       

        if (isSmall)
        {
            bigSize.gameObject.SetActive(false);
            smallSize.gameObject.SetActive(true);
            mediumSize.gameObject.SetActive(false);

        }

        if (isMedium)
        {
            bigSize.gameObject.SetActive(false);
            smallSize.gameObject.SetActive(false);
            mediumSize.gameObject.SetActive(true);

        }
    }


}
