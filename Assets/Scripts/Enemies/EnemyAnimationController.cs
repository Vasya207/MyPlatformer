using System;
using Helpers;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController: MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetAction(EnemyState enemyState)
        {
            switch (enemyState)
            {
                case EnemyState.Idle:
                    animator.SetBool(Constants.MovementBoolName, false);
                    break;
                case EnemyState.Movement:
                    animator.SetBool(Constants.MovementBoolName, true);
                    break;
                case EnemyState.MeleeAttack:
                    animator.SetTrigger(Constants.MeleeAttackTriggerName);
                    break;
                case EnemyState.ReceiveDamage:
                    animator.SetTrigger(Constants.HurtTriggerName);
                    break;
                case EnemyState.Death:
                    animator.SetTrigger(Constants.DeathTriggerName);
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