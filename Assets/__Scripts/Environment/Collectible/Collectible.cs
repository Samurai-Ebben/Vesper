using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var collectibleManager = GameManager.Instance.GetComponent<CollectibleManager>();

            collectibleManager.CollectibleCollected();
            collectibleManager.RegisterSelfAsCollected(gameObject);

            // TODO Animation/Particles

            gameObject.SetActive(false);
        }
    }
}
