using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController: MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private const string MovementBoolName = "moving";
        private const string MeleeAttackTriggerName = "meleeAttack";
        private const string HurtTriggerName = "hurt";
        private const string DeathTriggerName = "die";

        public void SetAction(EnemyState enemyState)
        {
            switch (enemyState)
            {
                case EnemyState.Idle:
                    animator.SetBool(MovementBoolName, false);
                    break;
                case EnemyState.Movement:
                    animator.SetBool(MovementBoolName, true);
                    break;
                case EnemyState.MeleeAttack:
                    animator.SetTrigger(MeleeAttackTriggerName);
                    break;
                case EnemyState.ReceiveDamage:
                    animator.SetTrigger(HurtTriggerName);
                    break;
                case EnemyState.Death:
                    animator.SetTrigger(DeathTriggerName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyState), enemyState, null);
            }
        }
        
        public enum EnemyState
        {
            Idle,
            Movement,
            MeleeAttack,
            ReceiveDamage,
            Death
        }
    }
}