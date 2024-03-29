using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderData;

public class animatorMC : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed = 1f;

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
    public float distance = 1f;
    public LayerMask RockMask;
    private bool isMovingRock;


    private string Idle = "idie";
    private string Walk = "walk";
    private string Run = "run";
    private string Crouch_idle = "Crouch idle";
    private string Crouch_Walk = "Crouch Walk";
    private string Jump = "Jump";
    private string Pushs = "pushs";
    private string Pushs_Idle = "push_Idle";

    GameObject rock;

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

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, RockMask);
        if (hit.collider != null && hit.collider.gameObject.tag == "pushable" && Input.GetKey(KeyCode.E))
        {
            rock = hit.collider.gameObject;

            rock.GetComponent<FixedJoint2D>().enabled = true;
            rock.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            rock.GetComponent<rockpull>().beingPushed = true;
            isMovingRock = true;

        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            rock.GetComponent<FixedJoint2D>().enabled = false;
            rock.GetComponent<rockpull>().beingPushed = false;
            isMovingRock = false;
        }
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
            //if (xAxis != 0 && isMovingRock == true && isCrouch == false && walkSpeed != 0)
            //{
                //ChangeAnimationState(Pushs);
            //}
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

        if (xAxis != 0 && isMovingRock == true && isCrouch == false && walkSpeed != 0)
        {
            ChangeAnimationState(Pushs);
        }
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
        else if (xAxis == 0 && isMovingRock == true)
        {
            ChangeAnimationState(Pushs_Idle);
        }
        else if (xAxis != 0 && isMovingRock == true)
        {
            ChangeAnimationState(Pushs);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }

}
