using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlSelectorManager : MonoBehaviour
{
    private void Start() {
        Time.timeScale = 1.0f;
    }

    public void GoToLevel(int levelNum) {
        SceneManager.LoadScene(levelNum);
    }
}
