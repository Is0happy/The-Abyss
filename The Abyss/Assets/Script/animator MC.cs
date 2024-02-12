using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class animatorMC : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;

    Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D Rigidbody2D;
    private bool iscrouchPressed;
    private bool isrunPressed;
    private bool iscrouching;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimation;
    private string currentState;

    [SerializeField]
    private float crouchDelay = 1.0f;

    private string Idle = "idie";
    private string Walk = "walk";
    private string Run = "run";
    private string Chrouch = "Chrouch";
    private string Crouch_idle = "Crouch idle";
    private string Crouch_Walk = "Crouch Walk";

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("left shift"))
        {
            isrunPressed = true;
        }
        if (Input.GetButtonDown("left ctrl"))
        {
            iscrouchPressed = true;
        }
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

        Vector2 vel = new Vector2(0, Rigidbody2D.velocity.y);
        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {

        }
    }
}
