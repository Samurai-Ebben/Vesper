using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Transitioning : MonoBehaviour
{
    public void GoToScene(int loadScene = 2)
    {
        int currentLvl = SceneManager.GetActiveScene().buildIndex;  
        if(loadScene <=1)
        SceneManager.LoadScene(1);
        else
        {
            PlayerController.instance.canMove = true;
            PlayerController.instance.GetComponentInChildren<SpriteRenderer>().enabled = true;
            SceneManager.LoadScene(currentLvl);
        }

    }
}
