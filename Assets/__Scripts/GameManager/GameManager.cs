using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player { get; private set; }

    private void Start()
    {
        if (instance != null) return;
        instance = this;
        
        player = PlayerController.player;
    }

}
