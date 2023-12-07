using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingMovement : MonoBehaviour , IReset
{

    public float targetHeight;
    public float riseSpeed = 2.0f;
    [Range(0,50)]public float durationOnTarget = 4;

    private Vector3 initialPosition;
    private bool isRising = false;

    private enum States {DOWN,UP };
    private States currentState;

    private float timer;


    void Start()
    {
        currentState = States.DOWN;
    }

    void Update()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.transform.parent.GetComponent<PlayerHandler>();
        if (player != null)
        {
            player.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.transform.parent.GetComponent<PlayerHandler>();
        if (player != null)
        {
            player.SetParent(null);
        }
    }

    public void Reset()
    {
        transform.position = initialPosition;
    }

    private void OnEnable()
    {
        initialPosition = transform.position;

        ResettableObjectManager.Instance?.RegisterObject(this);
    }

    private void OnDisable()
    {
        ResettableObjectManager.Instance?.UnregisterObject(this);
    }
}
