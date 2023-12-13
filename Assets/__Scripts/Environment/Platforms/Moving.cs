using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving : MonoBehaviour, IReset
{

    public List<Transform> coordinates;
    int currentIndex;
    
    public float waitDuration;
    public float speed = 1f;
    public float percentageDistance;
    [Range(0,1)] public float startPercentageDistance;

    Transform start;
    Transform end;

    bool move = true;
    Coroutine waitCoroutine;

    void Start()
    {
        RegisterSelfToResettableManager();
        InitialValues();
    }

    void FixedUpdate()
    {
        if (move)
        {
            percentageDistance += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start.position, end.position, percentageDistance);
        }

        if (percentageDistance >= 1)
        {
            waitCoroutine = StartCoroutine(Wait());
            NextCycle();
        }
    }

    void NextCycle()
    {
        percentageDistance = 0;
        start = end;
        currentIndex++;

        if (currentIndex >= coordinates.Count)
        {
            currentIndex = 0;
        }

        end = coordinates[currentIndex];
    }

    IEnumerator Wait()
    {
        move = false;
        yield return new WaitForSeconds(waitDuration);
        move = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerHandler = PlayerController.player.transform.parent.GetComponent<PlayerHandler>();
            if (playerHandler != null)
            {
                playerHandler.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerHandler = PlayerController.player.transform.parent.GetComponent<PlayerHandler>();
            if (playerHandler != null)
            {
                playerHandler.SetParent(null);
            }
        }
    }

    public void Reset()
    {
        InitialValues();
    }

    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance?.RegisterObject(this);
    }

    private void InitialValues()
    {
        start = coordinates[0];
        end = coordinates[1];
        currentIndex = 1;
        percentageDistance = startPercentageDistance;
        move = true;

        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
        }
    }
}

