﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [Header("Lazer")]
    [Range(0.1f, 2)]
    public float timeBetweenShots = 0.5f;
    public float range = 100f;

    [SerializeField] float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;

    LineRenderer gunLine;

    public float effectsDisplayTime = 0.2f;
    public bool startCooldown = false;

    [Header("Materials")]
    public Material green;
    public Material red;

    //bot
    GyroBot gyroScript;

    //pickup
    HealthPickup healthPickup;
    EnergyPickup energyPickup;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        healthPickup = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthPickup>();
        energyPickup = GameObject.FindGameObjectWithTag("Energy").GetComponent<EnergyPickup>();
    }
    void Update ()
    {
         
        if (startCooldown)
        {
            gunLine.material = green;
            timer += Time.deltaTime;
            if(timer >= timeBetweenShots)
            {
                gunLine.material = red;
                startCooldown = false;
                timer = 0f;
            }
        }
        
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        if (startCooldown == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                startCooldown = true;
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100f);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log(hits[i].collider.name);
                shootHit = hits[i];
                if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
                {
                    if (gyroScript = shootHit.collider.gameObject.GetComponent<GyroBot>())
                    {
                        gyroScript.SeekPlayer();
                    }
                    if (shootHit.collider.tag == "Health")
                    {
                        healthPickup.SeekPlayer();
                    }
                    if (shootHit.collider.tag == "Energy")
                    {
                        energyPickup.SeekPlayer();
                    }
                }
            }
        }
        
    }

}
