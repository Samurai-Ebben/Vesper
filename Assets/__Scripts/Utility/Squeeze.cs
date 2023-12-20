using UnityEngine;

public class Squeeze : MonoBehaviour
{
    [Range(0.00001f, 1f)] public float squeezedMultiplier;
    public float squeezeSpeed = 2.0f;
    public float returnSpeed = 4.0f;

    private Vector3 originalScale;
    private Vector3 squeezedScale;
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    public float deltaY { get; private set; }
    public bool isSqueezing;
    public bool isReturning;

    void Start()
    {
        originalScale = transform.localScale;

        squeezedScale = originalScale;
        squeezedScale.y *= squeezedMultiplier;
        //squeezeAmount *= transform.localScale.y;
        originalPosition = transform.position;

        deltaY = (originalScale.y - squeezedScale.y) / 2;
        targetPosition = transform.position - (transform.up * deltaY);
    }

    void Update()
    {
        if (isSqueezing)
        {
            transform.localScale = Vector3.Lerp(originalScale, squeezedScale, squeezeSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, squeezeSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localScale, squeezedScale) < 0.01f)
            {
                transform.localScale = squeezedScale;
                isSqueezing = false;
                isReturning = true;
            }
        }

        if (isReturning)
        {
            transform.localScale = Vector3.Lerp(squeezedScale, originalScale, returnSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(targetPosition, originalPosition, returnSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
            {
                transform.localScale = originalScale;
                transform.position = originalPosition;
                isReturning = false;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isSqueezing = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isSqueezing = false;
    //        isReturning = true;
    //    }
    //}
}
