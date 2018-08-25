using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmBot : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }
	
	void Update ()
    {
        agent.SetDestination(target.position);
    }
}
