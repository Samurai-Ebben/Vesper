using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelController LevelController { get; private set; }

    public GameObject player { get; private set; }

    private void Start()
    {
        if (instance != null) return;
        instance = this;
        LevelController = GetComponent<LevelController>();
        player = LevelController.player;
    }

}
