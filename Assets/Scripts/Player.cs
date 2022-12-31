using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [SerializeField] float startingHealth;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] LayerMask groundLayer;

    bool dead;
    Vector2 moveInput;
    public float currentHealth { get; private set; }

    Animator myAnimator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeet;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponentInChildren<BoxCollider2D>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        Run();
        FlipSprite();
        myAnimator.SetBool("grounded", isGrounded());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spikes")
        {
            TakeDamage(collision.GetComponent<Spikes>().TakeHealth());
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
            Debug.Log("Value is Pressed");
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
}
