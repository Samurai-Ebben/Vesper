using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    LevelController levelController;

    [HideInInspector]public GameObject player;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;

        levelController = GetComponent<LevelController>();
        player = levelController.player;
    }

}
