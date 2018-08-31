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
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        gyroBlade = gameObject.transform.GetChild(1).GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        eyeRend = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
        eyeRend.material = neonOrange;
    }
    private void Update()
    {
        if (!readyToAttack)
        {
            timer += Time.deltaTime;
            if (timer > attackCooldownSpeed)
            {
                readyToAttack = true;
                timer = 0;
            }
        }
        transform.LookAt(target);
        if (modeHacked)
        {
            gyroBlade.tag = ("Blade");
            eyeRend.material = neonBlue;
            hackedTimer += Time.deltaTime;
            if (hackedTimer > hackedLength)
            {
                gyroBlade.tag = ("safe");
                modeHacked = false;
                eyeRend.material = neonOrange;
                transform.LookAt(target);
                hackedTimer = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        agent.SetDestination(target.position);
        if (seekTime)
        {
            rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
            modeHacked = true;
            seekTime = false;
        }
    }
    public void SeekPlayer()
    {
        seekTime = true;
    }
    public void playerHasBeenHit()
    {
        readyToAttack = false;
    }
}