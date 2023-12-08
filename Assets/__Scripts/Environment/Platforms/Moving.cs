using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float waitDuration;

    public List<Transform> coordinates;
    int currentIndex;
    
    public float percentageDistance;
    public float speed = 1f;

    Transform start;
    Transform end;

    bool move = true;

    void Start()
    {
        start = coordinates[0];
        end = coordinates[1];
        currentIndex = 1;
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
            StartCoroutine(Wait());
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

}

