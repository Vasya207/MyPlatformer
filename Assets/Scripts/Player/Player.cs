using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [Header("Health Components")]
    [SerializeField] float startingHealth;

    [Header("Movement Components")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    [Header("Attack Components")]
    [SerializeField] float attackCooldown;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject[] arrows;

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;

    [Header("iFrames")]
    [SerializeField] float iFramesDuration;
    [SerializeField] float numberOfFlashes;

    public bool dead { get; private set; }
    Vector2 moveInput;
    public float currentHealth { get; private set; }
    float cooldownTimer = Mathf.Infinity;

    Animator myAnimator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeet;
    SpriteRenderer mySprite;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponentInChildren<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        Run();
        FlipSprite();

        cooldownTimer += Time.deltaTime;

        myAnimator.SetBool("grounded", isGrounded());

        if (Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        if (isGrounded())
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            myAnimator.SetTrigger("isJumping");
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (cooldownTimer > attackCooldown && canAttack())
        {
            myAnimator.SetTrigger("shoot");
            cooldownTimer = 0;

            arrows[FindArrow()].transform.position = firePoint.position;
            arrows[FindArrow()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    void Run()
    {
        myRigidBody.velocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.velocity.y);
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    public void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon; 
        if (playerHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            myAnimator.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                myAnimator.SetTrigger("die");
                myRigidBody.velocity = new Vector2(0,0);
                GetComponent<Player>().enabled = false;
                dead = true;
            }
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(myCapsuleCollider.bounds.center, 
            new Vector2(myCapsuleCollider.bounds.size.x - 0.2f, myCapsuleCollider.bounds.size.y - 0.2f),
            0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(9, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
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
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    public bool canAttack()
    {
        return myRigidBody.velocity == new Vector2(0, 0) && isGrounded();
    }
}
