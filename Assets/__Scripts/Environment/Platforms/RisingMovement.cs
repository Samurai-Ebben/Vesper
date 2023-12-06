using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingMovement : MonoBehaviour
{

    public float targetHeight;
    public float riseSpeed = 2.0f;

    private Vector3 initialPosition;
    public bool isRising = false;

    public enum States {DOWN,UP };
    public States currentState;

    public float timer;
    [Range(0,50)]public float durationOnTarget = 4;

    void Start()
    {
        initialPosition = transform.position;
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
}
