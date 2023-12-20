using UnityEngine;

public class TransformMatcher : MonoBehaviour
{
    public GameObject targetObject;

    private void Start()
    {
        GetComponent<Collider2D>().enabled = true;

        if (targetObject != null)
        { 
            transform.position = targetObject.transform.position;
            transform.rotation = targetObject.transform.rotation;
            transform.localScale = targetObject.transform.localScale;
        }
        else
        {
            Debug.LogError("Target object is not assigned!");
        }
    }
}