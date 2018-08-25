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

        [Header("COOLDOWN")]
        public float maxCooldown;
        public bool startCooldown;
        float cooldownTimer;

        Rigidbody rb;

        void Start()
        {
            originalSpeed = movementSpeed;
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();
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
            if (!startCooldown)
            {
                StartDash();
            }
        }
        void Dash()
        {
            if (dashing)
            {
                col.isTrigger = true;
                timer += Time.deltaTime;
                originalSpeed = dashSpeed; 
                if (timer > dashDistance)
                {
                    originalSpeed = movementSpeed;
                    dashing = false;
                    col.isTrigger = false;
                }
            }
        }
        public void StartDash()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timer = 0;
                dashing = true;
                startCooldown = true;
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
