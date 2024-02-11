using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Camera cam;
    public Transform subject;
    Vector2 starPosition;
    float starZ;
    Vector2 travel => (Vector2)cam.transform.position - starPosition;
    float distanceFormSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFormSubject > 0? cam.farClipPlane: cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFormSubject) / clippingPlane;

    
    public void Start()
    {
        starPosition = transform.position;
        starZ = transform.position.z;
    }

    public void Update()
    {
        Vector2 newPos = starPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, starZ);
        transform.position = new Vector3(newPos.x, newPos.y, starZ);
        
    }
}
