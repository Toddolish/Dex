using System.Collections;
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
    private Transform destination;


    [Header("References")]
    public GameObject projectile;
    public Transform target;
    // public Transform holder;
    public Transform[] holder;
    private int currentHolder = 0;
    public ParticleSystem particle;
    public enum behaviour { attack, seek }
    public NavMeshAgent memeBot;
    public GameObject[] pickUps;
    Rigidbody rb;

    #region Hacked
    [Header("HACKED")]
    public float forceSpeed;
    public bool modeHacked;//when in hacked mode eye will be blue therefore this enemy can now destroy other enemys
    public float hackedTimer;
    public float hackedLength;
    bool seekTime;

    //[Header("MATERIALS")]
    //MeshRenderer eyeRend;
    //public Material neonBlue;//eye colour when hacked by player
    //public Material neonOrange;//eye colour when hunting player
    #endregion


    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    //chase and attack condition
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > chasingRange)
        {
            Seek();
        }
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
        //if cool down is more than 0, can't attack and start cooling down
        if (coolDown > 0)
        {
            canAttack = false;
            coolDown -= Time.deltaTime;
        }
        #endregion
        //if current health is 0 or less, destroy self and spawn particles
        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
            Instantiate(particle, transform.position, transform.rotation);
            Instantiate(pickUps[Random.Range(0, 2)], transform.position, transform.rotation);
        }
        Hacked();
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
            tempRigidBodyProjectile.AddForce(holder[currentHolder].transform.forward * projectileSpeed);
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
    void Hacked()
    {
        if (modeHacked)
        {
            //eyeRend.material = neonBlue;
            hackedTimer += Time.deltaTime;
            if (hackedTimer > hackedLength)
            {
                modeHacked = false;
                //eyeRend.material = neonOrange;
                transform.LookAt(target);
                hackedTimer = 0;
            }
        }
    }
    public void SeekPlayer()
    {
        rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
        modeHacked = true;
    }
}