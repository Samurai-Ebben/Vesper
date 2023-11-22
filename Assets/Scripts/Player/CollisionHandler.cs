using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject mainSwitch;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovingController")
        {
            mainSwitch.transform.parent = collision.transform;
            //rb2D.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("Intagen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovingController")
        {
            mainSwitch.transform.parent = null;
            //rb2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
