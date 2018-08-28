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
    PlayerStats playerStats;
    Rigidbody rb;
    GyroBot gyroScript;

    [Header("HEALTH")]
    public float curHealth;
    public float maxHealth = 100f;
    public ParticleSystem explosionParticle;

    [Header("KNOCKBACK")]
    public float knockBackForce;
    public float damageTaken;

    [Header("ATTACK")]
    public bool readyToAttack = true;
    [SerializeField]float timer;
    public float attackCooldownSpeed;

    [Header("DROPS")]
    public GameObject EnergyPickup;
    public GameObject HealthPickup;

    public float minPickupCount;
    public float maxPickupCount;
    public float dropRate;

    void Start ()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        curHealth = maxHealth;
    }
	
	void Update ()
    {
        dropRate = Random.Range(minPickupCount, maxPickupCount);
        agent.SetDestination(target.position);
        transform.LookAt(target);
        Explode();

        if(!readyToAttack)
        {
            timer += Time.deltaTime;
            if(timer > attackCooldownSpeed)
            {
                readyToAttack = true;
                timer = 0;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Blade") //getting the gyro robot by a tag
        {
            //gyroScript = other.gameObject.GetComponent<GyroBot>();
            //if (gyroScript.modeHacked)
            //{
                curHealth -= 100f;
            //}
        }
        if (other.gameObject.tag == "Player")
        {
            TakeDamage();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (readyToAttack && collision.gameObject.tag == "Player")
        {
            playerStats.DamageBySwarmBot();
            readyToAttack = false;
        }
    }
    void Explode()
    {
        if (curHealth <= 0)
        {
            HealthDrop();
            EnergyDrop();
            curHealth = 0;
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage()
    {
        curHealth = curHealth - damageTaken;
        //rb.AddForce(-transform.forward * knockBackForce, ForceMode.Impulse);
        return;
    }
    public void SeekPlayer()
    {
        //rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
        //modeHacked = true;
    }
    void HealthDrop()
    {
        if(dropRate > 11 && dropRate < 12)
        {
            Instantiate(HealthPickup, transform.position, transform.rotation);
        }
    }
    void EnergyDrop()
    {
        if (dropRate > 20 && dropRate < 21)
        {
            Instantiate(EnergyPickup, transform.position, transform.rotation);
        }
    }
}
