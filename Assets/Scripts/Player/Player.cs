using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [Header("Health Component")]
    [SerializeField] float startingHealth;

    [Header("Movement Components")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;

    [Header("iFrames")]
    [SerializeField] float iFramesDuration;
    [SerializeField] float numberOfFlashes;

    bool dead;
    Vector2 moveInput;
    public float currentHealth { get; private set; }

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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platforms" || collision.gameObject.tag == "Interactable")
        {
            myAnimator.SetBool("grounded", isGrounded());
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isGrounded() == false) { return; }
        
        if (value.isPressed)
        {
            myAnimator.SetBool("grounded", false);
            myAnimator.SetTrigger("isJumping");
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(myCapsuleCollider.bounds.center, myCapsuleCollider.bounds.size,
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
}
