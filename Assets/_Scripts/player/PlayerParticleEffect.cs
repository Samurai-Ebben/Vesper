using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffect : MonoBehaviour
{

    public ParticleSystem pfx;
    public ParticleSystem trail;
    public ParticleSystem jumpFx;
    public ParticleSystem landFx;
    private PlayerController player;
    public float sizeOffset = 1.2f;
    public float trailOffset = 1.2f;

    public bool isJump = false;
    public bool isLand = false;
    private void Start()
    {
        player  = GetComponent<PlayerController>();      
    }

    private void Update()
    {
        ParticleSizeToPlayer();
    }

    private void ParticleSizeToPlayer()
    {
        ParticleSystem.ShapeModule pfxShape = pfx.shape;
        ParticleSystem.ShapeModule trailShape = trail.shape;
        trailShape.scale = player.transform.localScale * trailOffset;
        pfxShape.scale = player.transform.localScale * sizeOffset;
    }

    private void PlayEffects()
    {
        if(isJump)
            jumpFx.Play();
        if(isLand)
            landFx.Play();
    }
}
