using UnityEngine;

public class Squeeze : MonoBehaviour
{
    [Range(0, 0.999f)]public float squeezeAmount = 0.8f;
    public float squeezeSpeed = 2.0f;
    public float returnSpeed = 4.0f;

    private Vector3 originalScale;
    private Vector3 squeezedScale;
    private Vector3 originalPosition;
    private bool isSqueezing = false;
    private bool isReturning = false;

    private void Start()
    {
        originalScale = transform.localScale;
        squeezedScale = originalScale;
        squeezedScale.y *= squeezeAmount;
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isSqueezing)
        {
            float deltaY = (originalScale.y - squeezedScale.y) / 2;
            transform.localScale = Vector3.Lerp(transform.localScale, squeezedScale, squeezeSpeed * Time.deltaTime);
            transform.position = originalPosition + new Vector3(0, -deltaY, 0);

            if (Vector3.Distance(transform.localScale, squeezedScale) < 0.01f)
            {
                transform.localScale = squeezedScale;
                isSqueezing = false;
                isReturning = true;
            }
        }

        if (isReturning)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, returnSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, originalPosition, returnSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
            {
                transform.localScale = originalScale;
                transform.position = originalPosition;
                isReturning = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSqueezing)
        {
            isSqueezing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSqueezing = false;
            isReturning = true;
        }
    }
}
