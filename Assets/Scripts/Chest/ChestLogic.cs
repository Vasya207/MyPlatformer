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
        private GameSession gameSession;
        private SoundManager soundManager;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
            myBoxCollider = GetComponent<BoxCollider2D>();
            myParticleSystem = GetComponent<ParticleSystem>();
            gameSession = GameSession.Instance;
            myParticleSystem.Play();
            myAnimator.enabled = false;
            soundManager = SoundManager.Instance;
        }

        public void OpenTheChest()
        {
            gameSession.AddToScore(coinsInChest);
            myAnimator.enabled = true;
            myBoxCollider.enabled = false;
            myParticleSystem.Stop();
            soundManager.PlaySound(openSound);
        }
    }
}