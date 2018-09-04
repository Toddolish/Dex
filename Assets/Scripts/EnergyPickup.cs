﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EnergyPickup : MonoBehaviour
{
    Rigidbody rb;
    public float forceSpeed;
    public Transform target;
    bool seekTime;

    PlayerStats playerStats;
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (seekTime)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, forceSpeed * Time.deltaTime);
        }
    }
    public void SeekPlayer()
    {
        seekTime = true;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStats.curEnergy += 100;
            Destroy(this.gameObject);
        }
    }
}
