using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Camera cam;
    public Transform subject;
    Vector2 starPosition;
    float starZ;
    Vector2 travel => (Vector2).cam.transform.position - starPosition;
    Vector2 parallaxFactor;

    public void Start()
    {
        starPosition = transform.position;
        starZ = transform.position.z;
    }

    public void Update()
    {
        transform.position = starPosition + travel;
    }
}
