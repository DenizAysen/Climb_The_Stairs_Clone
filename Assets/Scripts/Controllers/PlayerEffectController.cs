using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameObject playerSweatingParticle;
    [SerializeField] private GameObject playerBloodExplosionParticle;

    [SerializeField] private Transform particlesParent;
    [SerializeField] private Transform particleRotation;
    #endregion
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayerHealth.OnDeath += DestroyAllParticles;
        PlayerController.OnReachedFinalStair += DestroyAllParticles;
    }

    public void CreateSweatingParticles(int amount)
    {
        if(particlesParent.childCount == 0)
        {
            GameObject sweatingParticleGMO = Instantiate(playerSweatingParticle, particlesParent.position, Quaternion.identity, particlesParent);
            sweatingParticleGMO.transform.localRotation = particleRotation.rotation;
            ParticleSystem ps = sweatingParticleGMO.GetComponent<ParticleSystem>();
            var emission = ps.emission;
            emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, amount) });
        }
        else
        {
            ParticleSystem ps = particlesParent.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            var emission = ps.emission;
            emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, amount) });
        }
    }
    public void DestroySweatingParticles()
    {
        if(particlesParent.childCount > 0)
        {
            Destroy(particlesParent.GetChild(0).gameObject);
        }
    }
    public void CreateBloodExplosionParticles()
    {
        Instantiate(playerBloodExplosionParticle, particlesParent.position, Quaternion.identity);
    }
    public void DestroyAllParticles()
    {
        if(particlesParent.childCount > 0)
        {
            for(int i = 0; i < particlesParent.childCount; i++)
            {
                Destroy(particlesParent.GetChild(i).gameObject);
            }
        }
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        PlayerHealth.OnDeath -= DestroyAllParticles;
        PlayerController.OnReachedFinalStair -= DestroyAllParticles;
    }
}
