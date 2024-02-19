using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pushobjects : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask RockMask;
    private string currentState;
    private Animator animator;
    private bool isGrounded;

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
            isGrounded = true;
            ChangeAnimationState(Pushs_Idle);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            rock.GetComponent<FixedJoint2D>().enabled = false;
            ChangeAnimationState(Idle);
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
