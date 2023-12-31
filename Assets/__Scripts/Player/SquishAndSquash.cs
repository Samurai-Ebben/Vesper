using System.Collections;
using UnityEngine;

public class SquishAndSquash : MonoBehaviour
{
    public float squashAmount = 0.2f;
    public float stretchAmount = 0.2f;

    public float squishSquashDuration = 0.2f;
    public float revertScaleDuration = 0.1f;

    private Vector3 originalScale;

    ScreenShakeHandler screenShakeHandler;
    RayCastHandler rayCastHandler;

    public bool squishAndSquashEnabled;

    void Start()
    {
        squishAndSquashEnabled = true;
        originalScale = transform.localScale;
        screenShakeHandler = Camera.main.GetComponent<ScreenShakeHandler>();
        rayCastHandler = GetComponentInParent<RayCastHandler>();
    }

    // Jump
    public void JumpSquash()
    {
        if (!squishAndSquashEnabled) return;
        StartCoroutine(SquishSquashOverTime(originalScale.x - stretchAmount, originalScale.y + stretchAmount));
    }

    // Land
    public void LandSquish()
    {
        if (!squishAndSquashEnabled) return;
        StartCoroutine(SquishSquashOverTime(originalScale.x + squashAmount, originalScale.y - squashAmount));
    }

    public void ToggleEnabled(bool boolean)
    {
        squishAndSquashEnabled = boolean;
    }

    IEnumerator SquishSquashOverTime(float targetX, float targetY)
    {
        Vector3 originalSize = transform.localScale;
        Vector3 targetSize = new Vector3(targetX, targetY, originalSize.z);

        float currentTime = 0.0f;

        while (currentTime <= squishSquashDuration)
        {
            transform.localScale = Vector3.Lerp(originalSize, targetSize, currentTime / squishSquashDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetSize;

        // Now, revert back to original size
        StartCoroutine(RevertOverTime(targetSize.x, targetSize.y, originalScale.x, originalScale.y));
    }

    IEnumerator RevertOverTime(float startX, float startY, float targetX, float targetY)
    {
        Vector3 startSize = new Vector3(startX, startY, transform.localScale.z);
        Vector3 targetSize = new Vector3(targetX, targetY, transform.localScale.z);

        float currentTime = 0.0f;

        while (currentTime <= revertScaleDuration)
        {
            transform.localScale = Vector3.Lerp(startSize, targetSize, currentTime / revertScaleDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Corner")
        {
            screenShakeHandler.CornerShake();
        }
    }
}
