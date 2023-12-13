using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GetComponent<CollectibleManager>().CollectibleCollected();

            // TODO Animation/Particles

            Destroy(gameObject);
        }
    }
}
