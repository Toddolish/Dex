using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    Rigidbody rb;
    public float forceSpeed;
    public Transform target;
    
    NavMeshAgent agent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }
    
    void Update()
    {
        transform.LookAt(target);
        //agent.SetDestination(target.position);
    }

    public void SeekPlayer()
    {
        rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
    }
}