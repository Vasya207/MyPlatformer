using System;
using System.Collections;
using Core;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Health Components")] [SerializeField]
        private float startingHealth = 3;

        [Header("Movement Components")] [SerializeField]
        private float moveSpeed = 10f;

        [SerializeField] private float jumpSpeed = 10f;

        [Header("Attack Components")] [SerializeField]
        private float attackCooldown;

        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject[] arrows;

        [Header("Layers")] [SerializeField] private LayerMask groundLayer;

        [Header("iFrames")] [SerializeField] private float iFramesDuration;
        [SerializeField] private float numberOfFlashes;

        [Header("SFX")] [SerializeField] private AudioClip arrowSound;
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip deathSound;

        public bool Dead { get; private set; }
        private Vector2 moveInput;
        public float CurrentHealth { get; private set; }
        private float cooldownTimer = Mathf.Infinity;

        private Rigidbody2D myRigidBody;
        private CapsuleCollider2D myCapsuleCollider;
        private SpriteRenderer mySprite;
        private PlayerInput playerInput;
        private PlayerCombat playerCombat;
        private PlayerAnimationController playerAnimationController;

        private void Awake()
        {
            myRigidBody = GetComponent<Rigidbody2D>();
            myCapsuleCollider = GetComponent<CapsuleCollider2D>();
            mySprite = GetComponent<SpriteRenderer>();
            playerInput = FindObjectOfType<PlayerInput>();
            playerCombat = FindObjectOfType<PlayerCombat>();
            playerAnimationController = GetComponent<PlayerAnimationController>();

            CurrentHealth = startingHealth;
        }

        private void Update()
        {
            Run();
            FlipSprite();

            cooldownTimer += Time.deltaTime;
            playerAnimationController.SetAction(PlayerAnimationController.PlayerState.Grounded, IsGrounded());
        }

        private void OnEnable()
        {
            Signals.OnDamagePlayer.AddListener(TakeDamage);
            Signals.OnHealthCollect.AddListener(AddHealth);
            Signals.OnLevelFinished.AddListener(DisablePlayerInput);
        }

        private void OnDisable()
        {
            Signals.OnDamagePlayer.RemoveListener(TakeDamage);
            Signals.OnHealthCollect.RemoveListener(AddHealth);
            Signals.OnLevelFinished.RemoveListener(DisablePlayerInput);
        }

        private void DisablePlayerInput()
        {
            if (playerInput != null) playerInput.enabled = false;
        }
        
        private void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        private void OnFire(InputValue value)
        {
            if (CanAttack() && playerCombat.AttackFinished())
            {
                SoundManager.Instance.PlaySound(arrowSound);
                playerAnimationController.SetAction(PlayerAnimationController.PlayerState.Shoot);
                cooldownTimer = 0;

                GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(); 
                if (projectile != null) {
                    projectile.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                    projectile.transform.position = firePoint.transform.position;
                    projectile.transform.rotation = firePoint.transform.rotation;
                    projectile.SetActive(true);
                }
            }
        }

        private void OnJump(InputValue value)
        {
            if (IsGrounded())
            {
                playerAnimationController.SetAction(PlayerAnimationController.PlayerState.Grounded, false);
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
                playerAnimationController.SetAction(PlayerAnimationController.PlayerState.IsJumping);
            }
        }

        private void Run()
        {
            var velocity = myRigidBody.velocity;
            velocity = new Vector2(moveInput.x * moveSpeed, velocity.y);
            myRigidBody.velocity = velocity;
            var playerHasHorizontalSpeed = Mathf.Abs(velocity.x) > Mathf.Epsilon;
            playerAnimationController.SetAction(PlayerAnimationController.PlayerState.Movement,
                playerHasHorizontalSpeed);
        }

        private IEnumerator OpenGameOverScreen()
        {
            yield return new WaitForSeconds(1);
            UIManager.Instance.GameOver();
        }

        private bool IsGrounded()
        {
            var raycastHit = Physics2D.BoxCast(myCapsuleCollider.bounds.center,
                new Vector2(myCapsuleCollider.bounds.size.x, myCapsuleCollider.bounds.size.y - 0.2f),
                0, Vector2.down, 0.1f, groundLayer);
            return raycastHit.collider != null;
        }

        private IEnumerator Invulnerability()
        {
            Physics2D.IgnoreLayerCollision(9, 11, true);
            for (var i = 0; i < numberOfFlashes; i++)
            {
                mySprite.color = new Color(1, 0.4283019f, 0.4489709f, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                mySprite.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }

            Physics2D.IgnoreLayerCollision(9, 11, false);
        }

        private int FindArrow()
        {
            for (var i = 0; i < arrows.Length; i++)
                if (!arrows[i].activeInHierarchy)
                    return i;
            return 0;
        }

        private bool CanAttack()
        {
            return myRigidBody.velocity == new Vector2(0, 0)
                   && IsGrounded()
                   && !Dead
                   && cooldownTimer > attackCooldown;
        }

        public void AddHealth(float value)
        {
            if (CurrentHealth >= 3) return;
            CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, startingHealth);
        }

        private void FlipSprite()
        {
            var playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed) transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

            if (CurrentHealth > 0)
            {
                playerAnimationController.SetAction(PlayerAnimationController.PlayerState.ReceiveDamage);
                SoundManager.Instance.PlaySound(hurtSound);
                StartCoroutine(Invulnerability());
            }
            else
            {
                if (!Dead)
                {
                    playerAnimationController.SetAction(PlayerAnimationController.PlayerState.Death);
                    myRigidBody.velocity = new Vector2(0, 0);
                    GetComponent<Player>().enabled = false;
                    DisablePlayerInput();
                    Dead = true;

                    SoundManager.Instance.PlaySound(deathSound);
                    StartCoroutine(OpenGameOverScreen());
                }
            }
        }
    }
}