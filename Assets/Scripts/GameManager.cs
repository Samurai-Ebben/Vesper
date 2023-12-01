using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    LevelController levelControl;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;

        levelControl = GetComponent<LevelController>();
    }

}
