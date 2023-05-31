using Core;
using UnityEngine;

namespace Chest
{
    public class ChestLogic : MonoBehaviour
    {
        [SerializeField] public int coinsInChest;
        [SerializeField] private AudioClip openSound;
        private Animator myAnimator;
        private BoxCollider2D myBoxCollider;
        private ParticleSystem myParticleSystem;

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
            GameSession.Instance.AddToScore(coinsInChest);
            myAnimator.enabled = true;
            myBoxCollider.enabled = false;
            myParticleSystem.Stop();
            SoundManager.Instance.PlaySound(openSound);
        }
    }
}