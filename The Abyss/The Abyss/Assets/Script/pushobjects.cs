using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pushobjects : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;

    public float distance = 1f;
    public LayerMask RockMask;
    private int groundMask;
    private string currentState;
    private Animator animator;
    private Rigidbody2D Rigidbody2D;
    private bool isGrounded;
    private bool isMovingRock;
    private float xAxis2;
    private bool isCrouch;

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
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");

    }

    // Update is called once per frame
    void Update()
    {
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

        xAxis2 = Input.GetAxis("Horizontal") * walkSpeed;

        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hitGround.collider != null)
        {
            isGrounded = true;
            //print("isground");
        }
        else
        {
            isGrounded = false;
            //print("notsiground");
        }


    }

    private void FixedUpdate()
    {
        if (xAxis2 == 0f && isMovingRock == true)
        {
            ChangeAnimationState(Pushs_Idle);
        }
        else if (xAxis2 != 0f && isMovingRock == true)
        {
            ChangeAnimationState(Pushs);
        }
        else if (isGrounded == true && isMovingRock == false)
        {
            if (xAxis2 != 0 && walkSpeed == 1)
            {
                ChangeAnimationState(Walk);
            }
            else if (xAxis2 != 0 && walkSpeed > 1)
            {
                ChangeAnimationState(Run);
            }
            else if (xAxis2 != 0 && walkSpeed < 1 && isCrouch == true)
            {
                ChangeAnimationState(Crouch_Walk);
            }
            else if (xAxis2 == 0 && isCrouch == true)
            {
                ChangeAnimationState(Crouch_idle);
            }
            else
            {
                ChangeAnimationState(Idle);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
