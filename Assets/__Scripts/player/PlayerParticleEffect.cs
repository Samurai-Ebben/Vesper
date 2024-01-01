using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerParticleEffect : MonoBehaviour
{
    ScreenShakeHandler cam;
    public ParticleSystem pfx;
    public ParticleSystem pfx2;
    public ParticleSystem trail;
    public ParticleSystem jumpFx;
    public ParticleSystem landFx;
    public ParticleSystem deathFx;
    private PlayerController player;
    public float sizeOffset = 1.2f;
    public float trailOffset = 1.2f;
    public float idleOffsetMulti = 1.2f;
    private void Start()
    {
        player  = GetComponent<PlayerController>();      
        cam = Camera.main.GetComponent<ScreenShakeHandler>();
    }

    private void Update()
    {
        ParticleSizeToPlayer();
    }

    private void ParticleSizeToPlayer()
    {
        ParticleSystem.ShapeModule pfxShape = pfx.shape;
        ParticleSystem.ShapeModule pfxShape2 = pfx2.shape;

        ParticleSystem.ShapeModule trailShape = trail.shape;
        trailShape.scale = player.transform.localScale * trailOffset;
        pfxShape.scale = player.transform.localScale * sizeOffset;
        pfxShape2.scale = player.transform.localScale * idleOffsetMulti;
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
        //pfx.Stop();
        //trail.Stop();
        deathFx.Play();    
    }
}
