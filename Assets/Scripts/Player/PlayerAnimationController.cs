using System;
using Helpers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetAction(PlayerState playerState, bool currentState = false)
        {
            switch (playerState)
            {
                case PlayerState.Idle:
                    animator.SetBool(Constants.MovementBoolName, false);
                    break;
                case PlayerState.Movement:
                    animator.SetBool(Constants.MovementBoolName, currentState);
                    break;
                case PlayerState.Attack:
                    animator.SetTrigger(Constants.AttackTriggerName);
                    break;
                case PlayerState.Shoot:
                    animator.SetTrigger(Constants.ShootTriggerName);
                    break;
                case PlayerState.ReceiveDamage:
                    animator.SetTrigger(Constants.HurtTriggerName);
                    break;
                case PlayerState.Grounded:
                    animator.SetBool(Constants.GroundedBoolName, currentState);
                    break;
                case PlayerState.IsJumping:
                    animator.SetTrigger(Constants.IsJumpingTriggerName);
                    break;
                case PlayerState.Death:
                    animator.SetTrigger(Constants.DeathTriggerName);
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

