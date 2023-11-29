using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    //public float waitDuration = 0.5f;

    public List<Transform> coordinates;
    int currentIndex;
    
    public float percentageDistance;
    public float speed = 1f;

    Transform start;
    Transform end;

    void Start()
    {
        start = coordinates[0];
        end = coordinates[1];
        currentIndex = 1;
    }

    void Update()
    {
        percentageDistance += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(start.position, end.position, percentageDistance);

        if (percentageDistance >= 1)
        {
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

