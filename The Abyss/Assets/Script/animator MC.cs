using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class animatorMC : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;

    Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D Rigidbody2D;
    private bool isJumpPressed = false;
    private float jumpForce = 25f;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimation;
    private string currentState;
    private bool isCrouch;

    [SerializeField]
    private float crouchDelay = 1.0f;

    private string Idle = "idie";
    private string Walk = "walk";
    private string Run = "run";
    private string Chrouch = "Chrouch";
    private string Crouch_idle = "Crouch idle";
    private string Crouch_Walk = "Crouch Walk";
    private string Jump = "Jump";

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isJumpPressed = true;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = 2f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            walkSpeed = 0.5f;
            isCrouch = true;
        }
        else
        {
            walkSpeed = 1f;
            isCrouch = false;
        }

        xAxis = Input.GetAxis("Horizontal") * walkSpeed;
        yAxis = Rigidbody2D.velocity.y;
        Rigidbody2D.velocity = new Vector2(xAxis , yAxis);
    }

    private void FixedUpdate()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Chack update movement based on input
        Vector2 vel = new Vector2(0, Rigidbody2D.velocity.y);
        
        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);
            
        }
        else
        {
            vel.x = 0;
            
            
        }
        
        if (isGrounded)
        {
            if (xAxis != 0 && walkSpeed == 1)
            {
                ChangeAnimationState(Walk);
            }
            else if (xAxis != 0 && walkSpeed > 1)
            {
                ChangeAnimationState(Run);
            }
            else if (xAxis != 0 && walkSpeed < 1 && isCrouch == true)
            {
                ChangeAnimationState(Crouch_Walk);
            }
            else if (xAxis == 0 && isCrouch == true)
            {
                ChangeAnimationState(Crouch_idle);
            }
            else
            {
                ChangeAnimationState(Idle);
            }
        }

        if (isJumpPressed == true && isGrounded == true)
        {
            Rigidbody2D.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(Jump);
        }

        if(xAxis != 0 && walkSpeed == 1)
        {
            ChangeAnimationState(Walk);
        }
        else if (xAxis != 0 && walkSpeed > 1)
        {
            ChangeAnimationState(Run);
        }
        else if (xAxis != 0 && walkSpeed < 1 && isCrouch == true)
        {
            ChangeAnimationState(Crouch_Walk);
        }
        else if (xAxis == 0 && isCrouch == true)
        {
            ChangeAnimationState(Crouch_idle);
        }
        else
        {
            ChangeAnimationState(Idle);
        }
        
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    
}
