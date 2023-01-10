using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLogic : MonoBehaviour
{
    [SerializeField] int coinsInChest;
    [SerializeField] AudioClip openSound;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;
    ParticleSystem myParticleSystem;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myParticleSystem = GetComponent<ParticleSystem>();
        myParticleSystem.Play();
        myAnimator.enabled = false;
    }

    public void OpenTheChest()
    {
        FindObjectOfType<GameSession>().AddToScore(coinsInChest);
        FindObjectOfType<CornerParticles>().PlayParticles();
        myAnimator.enabled = true;
        myBoxCollider.enabled = false;
        myParticleSystem.enableEmission = false;
        SoundManager.instance.PlaySound(openSound);
    }
}
