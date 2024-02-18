using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animatorMC : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;

    [Range(0f, 500f)]
    public float jumpForce = 100f;
    public int DeadsceneIndex;

    private Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D Rigidbody2D;
    private bool isJumpPressed;

    private int groundMask;
    private bool isGrounded;
    private string currentState;
    private bool isCrouch;


    private string Idle = "idie";
    private string Walk = "walk";
    private string Run = "run";
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
    }

    private void FixedUpdate()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
            //print("isground");
        }
        else
        {
            isGrounded = false;
            //print("notsiground");
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
        
        if (isGrounded == true)
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

        if (isJumpPressed == true && isGrounded == true && isCrouch == false)
        {
            Rigidbody2D.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(Jump);
        }

        Rigidbody2D.velocity = vel;

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

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
    
        public void Dead()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(DeadsceneIndex);
    }
    
}
