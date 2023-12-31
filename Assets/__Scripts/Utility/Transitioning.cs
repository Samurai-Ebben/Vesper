using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Transitioning : MonoBehaviour
{
    public void GoToScene(int loadScene = 3)
    {
        if (loadScene <= 2)
            SceneManager.LoadScene(2);
        else
        {
            PlayerController.instance.canMove = true;
            PlayerController.instance.GetComponentInChildren<SpriteRenderer>().enabled = true;
            PlayerController.instance.effects.enabled = true;

        }

    }
}
