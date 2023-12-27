using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Transitioning : MonoBehaviour
{
    public void GoToScene(bool loadOtherLevel = false)
    {
        int currentLvl = SceneManager.GetActiveScene().buildIndex;  
        if(loadOtherLevel)
        SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(currentLvl);

    }
}
