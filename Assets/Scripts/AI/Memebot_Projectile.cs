using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Memebot_Projectile : MonoBehaviour
{
    MemeBot memeScript;

    PlayerStats playerStatsScript;
    public ParticleSystem particle;




    public void OnCollisionEnter(Collision collision)
    {
     /* if (collision.transform.tag == "Player")
        {
            playerStatsScript.Damage();

        }
        */

        
        Destroy(gameObject);
        Instantiate(particle, transform.position, transform.rotation);
    }

}
