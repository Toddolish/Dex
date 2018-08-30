using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Memebot_Projectile : MonoBehaviour
{
    MemeBot memeScript;
    PlayerStats playerStatsScript;
    public ParticleSystem particle;


    private void Start()
    {
        memeScript = GameObject.Find("Memebot").GetComponent<MemeBot>();
        playerStatsScript = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    public void OnCollisionEnter(Collision collision)
    {
      if(collision.transform.tag == "Player")
      {
            playerStatsScript.DamageBySwarmBot();
      }
      Destroy(gameObject);
      Instantiate(particle, transform.position, transform.rotation);
    }

}
