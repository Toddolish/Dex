﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class Cannon : MonoBehaviour
{
    [Header("Lazer")]
    [Range(0.1f, 2)]
    public float timeBetweenShots = 0.5f;
    public float range = 100f;
    public float radius;
    public ParticleSystem ShockWave;
    public ParticleSystem ShockWaveDarkDex;
    public ParticleSystem DarkDexTrail;
    PlayerStats playerStats;

    [SerializeField] float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;

    //LineRenderer gunLine;

    public float effectsDisplayTime = 0.2f;
    public bool startCooldown = false;

    [Header("Materials")]
    public Material green;
    public Material red;

    //bot
    GyroBot gyroScript;
    SwarmBot swarmBot;
    MemeBot memeBot;

    //pickup
    HealthPickup healthPickup;
    EnergyPickup energyPickup;
    DarkDexPickup darkDexPickup;
    Parts partsPickup;

    void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        shootableMask = LayerMask.GetMask("Shootable");
        //gunLine = GetComponent<LineRenderer>();
    }
    void Update ()
    {
        if(playerStats.darkDexMode)
        {
            DarkDexTrail.Play();
            radius = 100;
        }
        else if(!playerStats.darkDexMode)
        {
            DarkDexTrail.Stop();
            radius = 5;
        }
        if (startCooldown)
        {
            //gunLine.material = green;
            timer += Time.deltaTime;
            if(timer >= timeBetweenShots)
            {
                //gunLine.material = red;
                startCooldown = false;
                timer = 0f;
            }
        }
        
        //gunLine.enabled = true;
        //gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        if (startCooldown == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (playerStats.darkDexMode)
                {
                    ShockWaveDarkDex.Play();
                    startCooldown = true;
                    Shoot();
                }
                if(!playerStats.darkDexMode)
                {
                    ShockWave.Play();
                    startCooldown = true;
                    Shoot();
                }
            }
        }
    }
    public void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("cannon");
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, radius, transform.forward, range, shootableMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                //Debug.Log(hits[i].collider.name); to check what i am hitting
                shootHit = hits[i];
                if (gyroScript = shootHit.collider.gameObject.GetComponent<GyroBot>())
                {
                    //gyroScript.modeHacked = false;
                    gyroScript.hackedTimer = 0;
                    gyroScript.ActivateSeekPlayer();
                }
                if (swarmBot = shootHit.collider.gameObject.GetComponent<SwarmBot>())
                {
                    swarmBot.SeekPlayer();
                }
				if (healthPickup = shootHit.collider.gameObject.GetComponent<HealthPickup>())
                {
					if (playerStats.curHealth != 100) 
					{
						healthPickup.SeekPlayer ();
					}
                }
                if (darkDexPickup = shootHit.collider.gameObject.GetComponent<DarkDexPickup>())
                {
                    darkDexPickup.SeekPlayer();
                }
                if (energyPickup = shootHit.collider.gameObject.GetComponent<EnergyPickup>())
                {
					if (playerStats.curEnergy != 100) 
					{
						energyPickup.SeekPlayer ();
					}
                }
                if (partsPickup = shootHit.collider.gameObject.GetComponent<Parts>())
                {
                    partsPickup.SeekPlayer();
                }
                if (memeBot = shootHit.collider.gameObject.GetComponent<MemeBot>())
                {
                    memeBot.SeekPlayer();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
