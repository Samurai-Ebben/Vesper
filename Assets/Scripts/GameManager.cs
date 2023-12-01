using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) return;
        instance = this;
    }
    private void Start()
    {
        print(SpawnAndCheckpoint.instance.player.name);

    }

}
