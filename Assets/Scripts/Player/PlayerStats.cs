using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("HEALTH")]
        public float curHealth;
        public float maxHealth;

        Image hpBar;
        public bool invincible;
        PlayerMovement playerMoveScript;


        void Start()
        {
            curHealth = maxHealth;
            hpBar = GameObject.Find("HpBar").GetComponent<Image>();
            playerMoveScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        void Update()
        {
            if(playerMoveScript.dashing)
            {
                invincible = true;
            }
            else
            {
                invincible = false;
            }
            hpBar.fillAmount = (curHealth / 100);
            GameOver();
        }
        private void OnTriggerEnter(Collider player)
        {
            if (player.gameObject.tag == "Blade")
            {
                curHealth = curHealth - 100;
            }
        }
        private void OnCollisionStay(Collision player)
        {
            if(player.gameObject.tag == "SwarmBot" && !invincible)
            {
                curHealth = curHealth - 1;
            }
        }
        void GameOver()
        {
            if (curHealth <= 0)
            {
                SceneManager.LoadScene("Prototype");
            }
        }
    }
}
