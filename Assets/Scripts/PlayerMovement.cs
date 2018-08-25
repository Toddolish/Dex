using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("SPEED")]
        public float originalSpeed;
        public float movementSpeed;
        public float dashSpeed;

        [Header("DASHING")]
        [Range(0.1f,2)]
        public float dashDistance;
        public bool dashing;
        public float timer;

        void Start()
        {
            originalSpeed = movementSpeed;
        }

        void FixedUpdate()
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            this.gameObject.transform.Translate(hor * originalSpeed, 0, ver * originalSpeed);
            Dash();
        }
        void Dash()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                dashing = true;
            }
            if (dashing)
            {
                timer += Time.deltaTime;
                originalSpeed = dashSpeed;
                if (timer > dashDistance)
                {
                    originalSpeed = movementSpeed;
                    dashing = false;
                    timer = 0;
                }
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Blade")
            {
                SceneManager.LoadScene("Prototype");
            }
        }
    }
}
