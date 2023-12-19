using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;

    public bool exitEventOn = false;
    public UnityEvent triggerExitEvent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggerEnterEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(exitEventOn && collision.CompareTag("Player"))
        {
            triggerExitEvent.Invoke();
        }
    }
}
