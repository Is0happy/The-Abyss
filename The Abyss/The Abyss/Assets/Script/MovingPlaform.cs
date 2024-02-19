using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MovingPlaform : MonoBehaviour
{
    public Transform posA, posB;

    public float speed;

    Vector3 targetPos;

    bool isTouch;

    private void Start()
    {
        targetPos = posA.position;
        isTouch = false;
    }

    private void Update()
    {

        if (isTouch == true)
        {
            targetPos = posB.position;
        }
        if (isTouch == false) 
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
        else if (collision.CompareTag("pushable"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = false;
        }
    }
}
