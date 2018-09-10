using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Memebot_Projectile : MonoBehaviour
{
    public GameObject player;
    PlayerStats playerStatsScript;
    public ParticleSystem particle;
    //LayerMask Ignorelayers;

    public void Start()
    {
        player = GameObject.Find("Player");
        playerStatsScript = player.GetComponent<PlayerStats>();
        //Physics.IgnoreLayerCollision(10, 30,true);
    }

    public void Update()
    {

    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStatsScript.DamageByMeme();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject);
        Instantiate(particle, transform.position, transform.rotation);
    }

}
