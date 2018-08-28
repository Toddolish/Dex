﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GyroBot : MonoBehaviour
{
    Rigidbody rb;
    public float forceSpeed;
    public Transform target;

    NavMeshAgent agent;
    Transform gyroBlade;
    public bool modeHacked;//when in hacked mode eye will be blue therefore this enemy can now destroy other enemys
    float hackedTimer;
    public float hackedLength;
    bool seekTime;

    [Header("MATERIALS")]
    MeshRenderer eyeRend;
    public Material neonBlue;//eye colour when hacked by player
    public Material neonOrange;//eye colour when hunting player

    [Header("HEALTH")]
    public float curHealth;
    public float maxHealth = 100;

    [Header("EXPLODE")]
    public GameObject gyroExplosion;
    ParticleSystem sparks;
    ParticleSystem smoke;


    void Start()
    {
        gyroBlade = gameObject.transform.GetChild(1).GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        eyeRend = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
        sparks = gameObject.transform.GetChild(2).GetComponentInChildren<ParticleSystem>();
        smoke = gameObject.transform.GetChild(3).GetComponentInChildren<ParticleSystem>();
        eyeRend.material = neonOrange;
        smoke.Stop();
        sparks.Stop();
        //health
        curHealth = maxHealth;
    }
    private void Update()
    {
        Explode();
        if(curHealth < 70)
        {
            sparks.Emit(2);
        }
        if(curHealth < 50)
        {
            smoke.Emit(1);
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
    void LateUpdate()
    {
        transform.LookAt(target);
        if(modeHacked)
        {
            gyroBlade.tag = ("Blade");
            eyeRend.material = neonBlue;
            hackedTimer += Time.deltaTime;
            if(hackedTimer > hackedLength)
            {
                gyroBlade.tag = ("safe");
                modeHacked = false;
                eyeRend.material = neonOrange;
                hackedTimer = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall" && modeHacked)
        {
            curHealth = curHealth - 34;
        }
    }

    public void SeekPlayer()
    {
        seekTime = true;
    }
    public void Explode()
    {
        if(curHealth <= 0)
        {
            curHealth = 0;
            Instantiate(gyroExplosion, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
        }
    }
}