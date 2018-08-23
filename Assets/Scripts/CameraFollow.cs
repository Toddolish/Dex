using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed;
	void Start ()
    {
		
	}
	
	void Update ()
    {
        Vector3 desiredPosision = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosision, smoothSpeed);
        transform.position = smoothedPosition;
            
	}
}
