﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MemeBot : MonoBehaviour
{

    [Header("Stats")]
    public float attackRange = 20f;
    public float chasingRange = 10f;
    public float projectileSpeed = 5f;
    public float coolDown;
    public float maxHealth = 5f;
    public float currentHealth;
    public float playerDamage = 1f;
    public bool canAttack = true;
    public float damage = 25f;



    [Header("References")]
    public GameObject projectile;
    public Transform target;
    // public Transform holder;
    public Transform[] holder;
    private int currentHolder = 0;
    public ParticleSystem particle;
    public enum behaviour { attack, seek }
    public NavMeshAgent memeBot;




    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    //chase and attack condition
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > chasingRange)
        { Seek(); }
        if (distance < chasingRange)
        {
            memeBot.SetDestination(transform.position);
        }

        //turn to look at target
        transform.LookAt(target);

        //getting the distance
        #region AttackCondition

        //if player distance is within attack range and you can attack, perform attack
        if ((distance <= attackRange) && (canAttack = true))
        {
            Attack();

        }
        if (coolDown > 0)
        {
            canAttack = false;
            coolDown -= Time.deltaTime;
        }
        #endregion
        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
            Instantiate(particle, transform.position, transform.rotation);
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Blade")
        {
            currentHealth -= currentHealth;
        }

    }
    void Attack()
    {
        //if cool down is 0 or less, fire a projectile and reset cool down.
        if (coolDown <= 0)
        {
            canAttack = true;
            GameObject tempprojectile = Instantiate(projectile, holder[currentHolder].position, holder[currentHolder].rotation) as GameObject;
            Rigidbody tempRigidBodyProjectile = tempprojectile.GetComponent<Rigidbody>();
            tempRigidBodyProjectile.AddForce(tempRigidBodyProjectile.transform.forward * projectileSpeed);
            coolDown = 1.5f;
            currentHolder++;
            if (currentHolder >= 3)
            {
                currentHolder = 0;
  }

        }

    }
    void Seek()
    {
        memeBot.SetDestination(target.position);

    }


}