using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HealthPickup : MonoBehaviour
{
    Rigidbody rb;
    public float forceSpeed;
    public Transform target;
    
    public bool modeHacked;//when in hacked mode eye will be blue therefore this enemy can now destroy other enemys
    float hackedTimer;
    public float hackedLength;
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
            transform.position =  Vector3.MoveTowards(transform.position, target.position, forceSpeed * Time.deltaTime);
        }
    }
    public void SeekPlayer()
    {
        seekTime = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStats.curHealth += 25;
            Destroy(this.gameObject);
        }
    }
}
