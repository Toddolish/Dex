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
        [Range(0.1f, 2)]
        public float dashDistance;
        public bool dashing;
        public float timer;
        CapsuleCollider col;
        public ParticleSystem dashParticle;
        public ParticleSystem darkDashParticle;
        public float dashConsuption;// the amount of energy taken when is dashing
        public GameObject dashRange; //enable dashRange to destroy swarmBots


        [Header("COOLDOWN")]
        public float maxCooldown;
        public bool startCooldown;
        float cooldownTimer;

        Rigidbody rb;
        PlayerStats mystats;
        [Header("Materials")]
        public Material defaultBlue;
        public Material DarkDexPurple;
        [Header("Renderers")]
        public MeshRenderer topBody;
        public MeshRenderer lowerBody;
        public MeshRenderer cannon;

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
            Dash();
            DashCooldown();
        }
        private void Update()
        {
            if (mystats.darkDexMode)
            {
                topBody.material = DarkDexPurple;
                lowerBody.material = DarkDexPurple;
                cannon.material = DarkDexPurple;
                //dashParticle.startColor = Color.cyan;
            }
            else if (!mystats.darkDexMode)
            {
                topBody.material = defaultBlue;
                lowerBody.material = defaultBlue;
                cannon.material = defaultBlue;
            }
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
                if (mystats.darkDexMode)
                {
                    darkDashParticle.Emit(5);
                }
                if (!mystats.darkDexMode)
                {
                    dashParticle.Emit(5);
                }
                if (timer > dashDistance)
                {
                    originalSpeed = movementSpeed;
                    dashing = false;
                    dashRange.SetActive(false);
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
