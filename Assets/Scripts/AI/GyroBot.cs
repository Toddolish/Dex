using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Player;

public class GyroBot : MonoBehaviour
{
    Rigidbody rb;
    public float forceSpeed;
    public Transform target;
    PlayerStats playerStats;

    NavMeshAgent agent;
    Transform gyroBlade;

    [Header("HEALTH")]
    public float curHealth;
    float maxHealth = 100;
    public bool canTakeDamage;
    public GameObject explosionParticle;
    ParticleSystem Flare;

    [Header("HACKED")]
    public bool modeHacked;//when in hacked mode eye will be blue therefore this enemy can now destroy other enemys
    public float hackedTimer;
    public float hackedLength;
    bool seekTime;

    [Header("MATERIALS")]
    MeshRenderer eyeRend;
    public Material neonBlue;//eye colour when hacked by player
    public Material neonOrange;//eye colour when hunting player

    [Header("ATTACK")]
    public bool readyToAttack = true;
    [SerializeField] float timer;
    public float attackCooldownSpeed;


    void Start()
    {
        curHealth = maxHealth;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        gyroBlade = gameObject.transform.GetChild(1).GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        eyeRend = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
        Flare = gameObject.transform.GetChild(2).GetComponentInChildren<ParticleSystem>();
        eyeRend.material = neonOrange;
    }
    private void Update()
    {
        Attack();
        Hacked();
        if(curHealth < 60)
        {
            Flare.Emit(1);
        }
        Explode();
    }
    public void OnCollisionExit(Collision other)
    {
       if(other.gameObject.tag == "Danger")
        {
            curHealth -= 55;
        }
    }
    private void FixedUpdate()
    {
        SeekPlayer();
    }
    public void ActivateSeekPlayer()
    {
        seekTime = true; //Activate seek mode 
    }
    public void playerHasBeenHit()
    {
        readyToAttack = false;
    }
    public void Attack()
    {
        agent.SetDestination(target.position);
        transform.LookAt(target); //always face the player target

        if (!readyToAttack) //cannot damage player until (ReadyToAttack = true)
        {
            timer += Time.deltaTime;
            if (timer > attackCooldownSpeed)
            {
                readyToAttack = true;
                timer = 0;
            }
        }
    }
    public void Hacked()
    {
        if (modeHacked)
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            agent.avoidancePriority = 99;
            gyroBlade.tag = ("Blade");
            eyeRend.material = neonBlue;
            hackedTimer += Time.deltaTime;
            if (hackedTimer > hackedLength)
            {
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                agent.avoidancePriority = 50;
                gyroBlade.tag = ("safe");
                modeHacked = false;
                eyeRend.material = neonOrange;
                transform.LookAt(target);
                hackedTimer = 0;
            }
        }
    }
    public void SeekPlayer()
    {
        if (seekTime)
        {
            rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse); //burst forward towards player
            modeHacked = true; //Activate hacked mode
            seekTime = false; //deactivate seek mode
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
}