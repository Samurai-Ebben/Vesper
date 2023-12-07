using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerParticleEffect : MonoBehaviour
{

    public ParticleSystem pfx;
    public ParticleSystem trail;
    public ParticleSystem jumpFx;
    public ParticleSystem landFx;
    public ParticleSystem deathFx;
    private PlayerController player;
    public float sizeOffset = 1.2f;
    public float trailOffset = 1.2f;

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

    public void CreateJumpDust()
    {
        jumpFx.Play();
    }

    public void CreateLandDust()
    {
        landFx.Play();
    }

    public void StopLandDust()
    {
        landFx.Stop();
    }

    public void DeathParticle()
    {
        pfx.Stop();
        trail.Stop();
        deathFx.Play();    
    }
}
