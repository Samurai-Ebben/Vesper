using UnityEngine;

public class SquishAndStretch : MonoBehaviour
{
    public float Stretch = 0.1f;
    [SerializeField] private Transform anchor;
    public Transform Sprite;

    private Rigidbody2D rb2d;
    private Vector3 originalScale;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalScale = Sprite.transform.localScale;

        if (!anchor)
            anchor = new GameObject(string.Format("_squash_{0}", name)).transform;
    }

    private void Update()
    {
        Sprite.parent = transform;
        Sprite.localPosition = Vector3.zero;
        Sprite.localScale = originalScale;
        Sprite.localRotation = Quaternion.identity;

        anchor.localScale = Vector3.one;
        anchor.position = transform.position;

        Vector3 velocity = rb2d.velocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            anchor.rotation = Quaternion.FromToRotation(Vector3.right, velocity);
        }

        var scaleX = 1.0f + (velocity.magnitude * Stretch);
        var scaleY = 1.0f / scaleX;
        Sprite.parent = anchor;
        anchor.localScale = new Vector3(scaleX, scaleY, 1.0f);
    }
}