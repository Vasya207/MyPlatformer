using System;
using Chest;
using Core;
using Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Attack Components")] 
        [SerializeField]
        private int damageAmount = 2;

        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private Transform attackPoint;

        [Header("Layers")] [SerializeField] 
        private LayerMask enemyLayer;

        [Header("Audio Components")] [SerializeField]
        private AudioClip hitAudio;

        [SerializeField] private AudioClip swordWavingAudio;

        private float cooldownTimer = Mathf.Infinity;
        private PlayerAnimationController playerAnimationController;
        private Animator animator;
        private const string CurrentAttackAnimName = "Player Light Attack";

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerAnimationController = GetComponent<PlayerAnimationController>();
        }

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        private void OnHit(InputValue inputValue)
        {
            if (cooldownTimer > attackCooldown)
            {
                cooldownTimer = Mathf.Epsilon;
                playerAnimationController.SetAction(PlayerAnimationController.PlayerState.Attack);
            }
        }
        
        /// <summary>
        /// Is triggered by Animator events
        /// </summary>
        
        private void Attack()
        {
            var hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            SoundManager.instance.PlaySound(swordWavingAudio);

            foreach (var hitObject in hitObjects)
                if (hitObject.CompareTag("Enemy"))
                {
                    SoundManager.instance.PlaySound(hitAudio);
                    hitObject.GetComponent<Enemy>().TakeDamage(damageAmount);
                }
                else if (hitObject.CompareTag("Chest"))
                {
                    hitObject.GetComponent<ChestLogic>().OpenTheChest();
                }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public bool AttackFinished()
        {
            return !animator.GetCurrentAnimatorStateInfo(0).IsName(CurrentAttackAnimName);
        }
    }
}