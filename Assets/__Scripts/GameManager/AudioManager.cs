using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    PlayerController player;

    public AudioClip jumpSmall, jumpBig, jumpMedium, switchToLarge, switchToSmall, switchToMedium;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();   
        player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
