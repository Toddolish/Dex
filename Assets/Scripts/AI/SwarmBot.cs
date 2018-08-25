using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Player;

public class SwarmBot : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    PlayerMovement playerMoveScript;
    Rigidbody rb;

    [Header("HEALTH")]
    public float curHealth;
    public float maxHealth = 100f;
    public ParticleSystem explosionParticle;

    [Header("KNOCKBACK")]
    public float knockBackForce;
    public float damageTaken;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        curHealth = maxHealth;
    }
	
	void Update ()
    {
        agent.SetDestination(target.position);
        transform.LookAt(target);
        Explode();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blade")
        {
            curHealth -= 100f;
        }
        if (other.gameObject.tag == "Player")
        {
            TakeDamage();
        }
    }
    void Explode()
    {
        if (curHealth <= 0)
        {
            curHealth = 0;
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage()
    {
        curHealth = curHealth - damageTaken;
        rb.AddForce(-transform.forward * knockBackForce, ForceMode.Impulse);
        return;
    }
}
