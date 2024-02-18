using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pushobjects : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask RockMask;

    GameObject rock;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, RockMask);
        if (hit.collider != null && Input.GetKey(KeyCode.E))
        {
            rock = hit.collider.gameObject;

            rock.GetComponent<FixedJoint2D>().enabled = true;
            rock.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            print("yes");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
