using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.VersionControl.Asset;

public class RisingMovement : MonoBehaviour , IReset
{
    private enum States {DOWN, UP};

    public float targetHeight;
    private Vector3 initialPosition;
    private States currentState;
    private float timer;

    public GameObject shakeTrigger;
    [HideInInspector]public ParticleSystem rocks;

    [Range(1, 10)] public float TimerToTarget = 4; // Represents time to reach target
    [Range(0, 50)] public float durationOnTarget = 2; // Represents duration at target height

    private float riseSpeed; 


    void Start()
    {
        rocks = GetComponentInChildren<ParticleSystem>();
        shakeTrigger.transform.position = new Vector3(transform.position.x,targetHeight);
        shakeTrigger.transform.parent = transform.parent;
        initialPosition = transform.position;
        currentState = States.DOWN;
        RegisterSelfToResettableManager();

        riseSpeed = (targetHeight - initialPosition.y) / TimerToTarget;
    }

    void Update()
    {
        Movement();
    }
    
    void Movement()
    {
        timer -= Time.deltaTime;
        switch (currentState)
        {
            case States.DOWN:
                if (timer > 0) return;

                if (transform.position.y > initialPosition.y)
                {
                    float step = riseSpeed * Time.deltaTime;
                    transform.Translate(Vector3.down * step);
                }                
                break;

            case States.UP:                
                if (transform.position.y < targetHeight)
                {
                    float step = riseSpeed * Time.deltaTime;
                    transform.Translate(Vector3.up * step);
                }
                break;               
        }
        return;

    }

    public void Rise()
    {
        currentState = States.UP;
        Debug.Log("UPP");
        return;
    }

    public void Descend()
    {
        if (currentState == States.DOWN)
        {
            return;
        }
        currentState = States.DOWN;
        timer = durationOnTarget;
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    var player = other.transform.parent.GetComponent<PlayerHandler>();
    //    if (player != null)
    //    {
    //        player.SetParent(transform);
    //    }
       
    //}
    //private void OnCollisionExit2D(Collision2D other)
    //{
        
    //    var player = other.transform.parent.GetComponent<PlayerHandler>();
    //    if (player != null)
    //    {
    //        player.SetParent(null);
    //    }
    //}

    public void ResetTimer()
    {
        timer = 0;
    }

    public void Reset()
    {
        timer = 0;
        transform.position = initialPosition;
        currentState = States.DOWN;
    }

    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance?.RegisterObject(this);
    }
}
