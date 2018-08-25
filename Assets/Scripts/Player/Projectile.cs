using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public float forceSpeed;
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * forceSpeed, ForceMode.Impulse);
    }
	
	void Update ()
    {
    }
}
