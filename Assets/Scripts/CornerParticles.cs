using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerParticles : MonoBehaviour
{
    ParticleSystem myParticleSystem;

    private void Awake()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        myParticleSystem.enableEmission = false;
    }

    public void PlayParticles()
    {
        myParticleSystem.enableEmission = true;
        myParticleSystem.Play();
    }
}
