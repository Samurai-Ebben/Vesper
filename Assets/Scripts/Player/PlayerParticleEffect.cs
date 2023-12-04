using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffect : MonoBehaviour
{

    private ParticleSystem pfx;
    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        pfx = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        ParticleSizeToPlayer();
    }

    private void ParticleSizeToPlayer()
    {
        ParticleSystem.ShapeModule pfxShape = pfx.shape;
        pfxShape.scale = player.transform.localScale;
    }
}
