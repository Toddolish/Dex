using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public NavMeshAgent agent;
    Rigidbody myrb;
    public float burstForce;
    public float rotateSpeed;
    public Transform target;
    void Start()
    {

        myrb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float step = burstForce * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
    }
    public void ClickedOn()
    {
        myrb.AddForce(0, 0, rotateSpeed, ForceMode.Impulse);
    }

}