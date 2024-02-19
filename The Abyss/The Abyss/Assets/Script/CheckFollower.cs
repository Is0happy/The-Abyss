using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFollower : MonoBehaviour
{
    public Transform leadingPlatform;

    private Vector3 offset;

    void Start()
    {
        if (leadingPlatform != null)
        {
            offset = transform.position - leadingPlatform.position;
        }
    }

    void Update()
    {
        if (leadingPlatform != null)
        {
            transform.position = leadingPlatform.position + offset;
        }
    }
}
