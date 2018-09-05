using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Player;

public class SwarmBot : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Rigidbody rb;

    PlayerMovement playerMoveScript;
    PlayerStats playerStats;
    
    GyroBot gyroScript;
    [Header("TYPE")]
    [Header("Gold swarmBot will drop gold Multipliers")]
    public bool goldSwarmBot;


    #region Health
    [Header("HEALTH")]
    public float curHealth;
    public float maxHealth = 100f;
    public ParticleSystem explosionParticle;
    #endregion
    #region Knockback
    [Header("KNOCKBACK")]
    public float knockBackForce;
    public float damageTaken;
    #endregion
    #region Attack    
    [Header("ATTACK")]
    public bool readyToAttack = true;
    [SerializeField]float timer;
    public float attackCooldownSpeed;
    #endregion
    #region Drops
    [Header("DROPS")]
    public GameObject energyPickup;
    public GameObject healthPickup;
    public GameObject nutPickup;
    public GameObject boltPickup;
    public GameObject goldPickup;
    public float minPickupCount;
    public float maxPickupCount;
    public float dropRate;
    #endregion
    #region Hacked
    [Header("HACKED")]
    public float forceSpeed;
    public bool modeHacked;//when in hacked mode eye will be blue therefore this enemy can now destroy other enemys
    public float hackedTimer;
    public float hackedLength;
    bool seekTime;
    
    [Header("MATERIALS")]
    MeshRenderer eyeRend;
    public Material neonBlue;//eye colour when hacked by player
    public Material neonOrange;//eye colour when hunting player
    #endregion



    void Start ()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        curHealth = maxHealth;
        eyeRend = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
    }
	
	void Update ()
    {
        dropRate = Random.Range(minPickupCount, maxPickupCount);
        agent.SetDestination(target.position);
        transform.LookAt(target);
        #region Methods
        Explode();
        Attack();
        #endregion
        if (modeHacked)
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            agent.avoidancePriority = 99;
            //gyroBlade.tag = ("Blade");
            eyeRend.material = neonBlue;
            hackedTimer += Time.deltaTime;
            if (hackedTimer > hackedLength)
            {
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                agent.avoidancePriority = 50;
                //gyroBlade.tag = ("safe");
                modeHacked = false;
                eyeRend.material = neonOrange;
                transform.LookAt(target);
                hackedTimer = 0;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Blade") //getting the gyro robot by a tag
        {
            curHealth -= 100f;
        }
        if (other.gameObject.tag == "Danger")
        {
            curHealth -= 100f;
        }
        if (other.gameObject.tag == "PlayerDash")
        {
            TakeDamage();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (readyToAttack && collision.gameObject.tag == "Player" && playerStats.invincible == false)
        {
            playerStats.DamageBySwarmBot();
            readyToAttack = false;
        }
    }
    void Explode()
    {
        if (curHealth <= 0)
        {
            //swarmBot will drop normalDrops
            if (goldSwarmBot == false)
            {
                HealthDrop();
                EnergyDrop();
                NutDrop();
                BoltDrop();
                curHealth = 0;
                Instantiate(explosionParticle, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            //swarmBot will drop goldDrops multipliers
            if(goldSwarmBot == true)
            {
                curHealth = 0;
                GoldDrop();
                //spawn new cool goldenLava explosion
                Instantiate(explosionParticle, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
    public void TakeDamage()
    {
        curHealth = curHealth - damageTaken;
        //rb.AddForce(-transform.forward * knockBackForce, ForceMode.Impulse);
        return;
    }
    public void SeekPlayer()
    {
        rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
        modeHacked = true;
    }
    void HealthDrop()
    {
        if(dropRate > 11 && dropRate < 12)
        {
            Instantiate(healthPickup, transform.position, transform.rotation);
        }
    }
    void EnergyDrop()
    {
        if (dropRate > 20 && dropRate < 21)
        {
            Instantiate(energyPickup, transform.position, transform.rotation);
        }
    }
    void NutDrop()
    {
        if (dropRate < 20)
        {
            Instantiate(nutPickup, transform.position, transform.rotation);
        }
    }
    void GoldDrop()
    {
        if (dropRate <= 20)
        {
            Instantiate(goldPickup, transform.position, transform.rotation);
        }
    }
    void BoltDrop()
    {
        if (dropRate < 20)
        {
            Instantiate(boltPickup, transform.position, transform.rotation);
        }
    }
    void Attack()
    {
        if (!readyToAttack)
        {
            timer += Time.deltaTime;
            if (timer > attackCooldownSpeed)
            {
                readyToAttack = true;
                timer = 0;
            }
        }
    }
}
