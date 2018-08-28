using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    

    private Camera cam;

	void Start ()
    {
        cam = GetComponent<Camera>();
	}
	
	void FixedUpdate ()
    {
        Vector3 desiredPosision = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosision, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
