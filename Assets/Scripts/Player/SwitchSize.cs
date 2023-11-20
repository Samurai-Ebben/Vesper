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
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentGameObject);

        if (isBig) 
        {
            bigSize.gameObject.SetActive(true);
            transform.position = bigSize.transform.position;
            Debug.Log("Changing..." + currentGameObject);
        }
       

        if (isSmall)
        {
            smallSize.gameObject.SetActive(true);
            Debug.Log("Changing..." + currentGameObject);
        }

        if (isMedium)
        {
            mediumSize.gameObject.SetActive(true);
            Debug.Log("Changing..." + currentGameObject);
        }
    }


}
