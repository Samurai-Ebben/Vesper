using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnBounce : MonoBehaviour
{
    [Header("Move Top")]
    public float MoveDistanceY;
    public float squeezeDuration;
    public float returnDuration;
    private Vector3 startPosition;
    private Vector3 endPosition;

    [HideInInspector]
    public float bounceForce;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position - new Vector3(0, MoveDistanceY, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        endPosition = transform.position - new Vector3(0, MoveDistanceY, 0);

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveDownUp());
        }
    }

    IEnumerator MoveDownUp()
    {
        MoveDown();
        yield return new WaitForSeconds(squeezeDuration);
        MoveUp();
    }
    void MoveDown()
    {
        transform.DOMove(endPosition, squeezeDuration);
    }
    void MoveUp()
    {
        transform.DOMove(startPosition, returnDuration);
    }
}
