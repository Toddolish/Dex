using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("SPEED")]
        public float originalSpeed;
        public float movementSpeed;
        public float dashSpeed;
        public float gravity;

        [Header("DASHING")]
        [Range(0.1f,2)]
        public float dashDistance;
        public bool dashing;
        public float timer;
        CapsuleCollider col;
        public ParticleSystem dashParticle;
        public float dashConsuption;// the amount of energy taken when is dashing
        public GameObject dashRange; //enable dashRange to destroy swarmBots

        [Header("COOLDOWN")]
        public float maxCooldown;
        public bool startCooldown;
        float cooldownTimer;

        Rigidbody rb;
        PlayerStats mystats;

        void Start()
        {
            dashRange.SetActive(false);
            originalSpeed = movementSpeed;
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();
            mystats = GameObject.Find("Player").GetComponent<PlayerStats>();
        }

        void FixedUpdate()
        {
            rb.AddForce(-transform.up * gravity, ForceMode.Acceleration);
            float hor = Input.GetAxis("Horizontal") * Time.deltaTime;
            float ver = Input.GetAxis("Vertical") * Time.deltaTime;
            this.gameObject.transform.Translate(hor * originalSpeed, 0, ver * originalSpeed);
            dashParticle = GameObject.Find("Dash_Particle").GetComponent<ParticleSystem>();
            Dash();
            DashCooldown();
        }
        private void Update()
        {
            if (!startCooldown)
            {
                StartDash();
            }
        }
        void Dash()
        {
            if (dashing)
            {
                dashRange.SetActive(true);
                col.radius = 0.5f; //clean this witha float string please todd
                timer += Time.deltaTime;
                originalSpeed = dashSpeed;
                dashParticle.Emit(5);
                if (timer > dashDistance)
                {
                    dashRange.SetActive(false);
                    originalSpeed = movementSpeed;
                    dashing = false;
                    //col.isTrigger = false;
                    col.radius = 0.5f;
                }
            }
        }
        public void StartDash()
        { 
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                if (mystats.curEnergy > 0)
                {
                    timer = 0;
                    dashing = true;
                    startCooldown = true;
                    mystats.curEnergy -= dashConsuption;
                    mystats.WaitForEnergyRegen();
                }
             }
        }
        public void DashCooldown()
        {
            if(startCooldown)
            {
                cooldownTimer += Time.deltaTime;
                if(cooldownTimer > maxCooldown)
                {
                    startCooldown = false;
                    cooldownTimer = 0;
                }
            }
        }
    }
}
