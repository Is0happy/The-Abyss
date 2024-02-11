using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 myPosition;
    private Animator animator;
    public Animation Animation;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myPosition = Vector3.zero;
        myPosition.x = Input.GetAxisRaw("Horizontal");
        myPosition.y = Input.GetAxis("Vertical");
        if(myPosition != Vector3.zero)
        {
            MoveCharater();
            animator.SetFloat("moveX", myPosition.x);
            animator.SetFloat("moveY", myPosition.y);
        }
    }

    void MoveCharater()
    {
        myRigidbody.MovePosition(transform.position + myPosition * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D target)
    {

    }
}
