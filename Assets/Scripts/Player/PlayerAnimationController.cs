using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private const string MovementBoolName = "isRunning";
        private const string AttackTriggerName = "attack";
        private const string ShootTriggerName = "shoot";
        private const string HurtTriggerName = "hurt";
        private const string GroundedBoolName = "grounded";
        private const string IsJumpingTriggerName = "isJumping";
        private const string DeathTriggerName = "die";
        
        public void SetAction(PlayerState playerState, bool currentState = false)
        {
            switch (playerState)
            {
                case PlayerState.Idle:
                    animator.SetBool(MovementBoolName, false);
                    break;
                case PlayerState.Movement:
                    animator.SetBool(MovementBoolName, currentState);
                    break;
                case PlayerState.Attack:
                    animator.SetTrigger(AttackTriggerName);
                    break;
                case PlayerState.Shoot:
                    animator.SetTrigger(ShootTriggerName);
                    break;
                case PlayerState.ReceiveDamage:
                    animator.SetTrigger(HurtTriggerName);
                    break;
                case PlayerState.Grounded:
                    animator.SetBool(GroundedBoolName, currentState);
                    break;
                case PlayerState.IsJumping:
                    animator.SetTrigger(IsJumpingTriggerName);
                    break;
                case PlayerState.Death:
                    animator.SetTrigger(DeathTriggerName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerState), playerState, null);
            }
        }
        
        public enum PlayerState
        {
            Idle,
            Movement,
            Attack,
            Shoot,
            ReceiveDamage,
            Grounded,
            IsJumping,
            Death
        }
    }
}

