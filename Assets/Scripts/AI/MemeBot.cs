using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeBot : MonoBehaviour
{
    
    [Header("Stats")]
    public float attackRange = 20f;
    public float projectileSpeed = 1000f;
    public float coolDown;
    public float maxHealth = 1f;
    public float currentHealth;
    public float playerDamage = 3f;
    public float gyroDamage = 3f;
    public float damage = 25f;
    public bool canAttack = true;

    [Header("References")]
    public GameObject projectile;
    public Transform target;
    public Transform holder;

    public enum behaviour { attack, idle }
    public ParticleSystem particle;



    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        //getting the distance
        #region AttackCondition
        float distance = Vector3.Distance(target.transform.position, transform.position);
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
        if (collision.transform.tag == "Player")
        {
            currentHealth -= playerDamage;
        }
        if (collision.transform.tag == "Gyro")
        {
            currentHealth -= gyroDamage;
        }
    }
    void Attack()
    {
        if (coolDown <= 0)
        {
            canAttack = true;
            GameObject tempprojectile = Instantiate(projectile, holder.position, transform.rotation) as GameObject;
            Rigidbody tempRigidBodyProjectile = tempprojectile.GetComponent<Rigidbody>();
            tempRigidBodyProjectile.AddForce(tempRigidBodyProjectile.transform.forward * projectileSpeed);
            coolDown = 1.5f;

        }

    }


}