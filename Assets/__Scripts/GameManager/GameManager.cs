using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelController LevelController { get; private set; }

    public GameObject Player { get; private set; }

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        LevelController = GetComponent<LevelController>();
        Player = LevelController.Player;

    }

}
