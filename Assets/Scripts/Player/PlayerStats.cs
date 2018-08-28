﻿using System.Collections;
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
        public float xHealth;
        public float xReductionSpeed;
        Image xHealthBar;
        Image hpBar;

        [Header("ENERGY")]
        public float curEnergy;
        public float maxEnergy;
        public float xEnergy; //the health behind the health to show indication of how much health you lost
        public float energyRegenSpeed; //the speed of the regeneration
        public float maxEnergyRegenTime; //how long it takes after dash before start regeneration
        public float energyTimerSpeed;
        public float energyTimer;
        public bool waitForRegen;
        Image xEnergyBar; 
        Image energyBar;

        public bool invincible;
        PlayerMovement playerMoveScript;


        void Start()
        {
            playerMoveScript = GameObject.Find("Player").GetComponent<PlayerMovement>();

            #region Health
            xHealth = maxHealth;
            curHealth = maxHealth;
            hpBar = GameObject.Find("HpBar").GetComponent<Image>();
            xHealthBar = GameObject.Find("HpBarMid").GetComponent<Image>();

            #endregion
            #region Energy
            energyTimer = maxEnergyRegenTime;
            xEnergy = maxEnergy;
            curEnergy = maxEnergy;
            energyBar = GameObject.Find("EnergyBar").GetComponent<Image>();
            xEnergyBar = GameObject.Find("EnergyBar_Mid").GetComponent<Image>();

            #endregion
        }

        void Update()
        {
            if (playerMoveScript.dashing)
            {
                invincible = true;
            }
            else
            {
                invincible = false;
            }
            #region Health
            hpBar.fillAmount = (curHealth / 100);
            xHealthBar.fillAmount = (xHealth / 100);

            if (xHealth > curHealth)
            {
                xHealth -= Time.deltaTime * xReductionSpeed;
                if (xHealth <= curHealth)
                {
                    xHealth = curHealth;
                }
            }
            GameOver();
            #endregion
            #region Energy
            //
            energyBar.fillAmount = (curEnergy / 100);
            xEnergyBar.fillAmount = (xEnergy / 100);

            //
            if (xEnergy > curEnergy)
            {
                xEnergy -= Time.deltaTime * xReductionSpeed;
                if (xEnergy <= curEnergy)
                {
                    xEnergy = curEnergy;
                }
            }

            //if waiting for regen energyTimer is decreasing
            if (waitForRegen)
            {
                energyTimer -= Time.deltaTime;
            }

            //if not waiting for regeneration
            if (!waitForRegen)
            {
                curEnergy += Time.deltaTime * energyRegenSpeed;
                xEnergy += Time.deltaTime * energyRegenSpeed;
            }
            //once  energyTimer is less then 0 you can now start regeneration
            if(energyTimer < 0)
            {
                waitForRegen = false;
            }
            //curEnergy and xEnergy will never go above maxEnergy
            if (curEnergy > maxEnergy || xEnergy > maxEnergy)
            {
                curEnergy = maxEnergy;
                xEnergy = maxEnergy;
            }

            //if energy time is greater then mxEnergyRegenTime then it will not exceed mxEnergyRegenTime
            if (energyTimer > maxEnergyRegenTime)
            {
                energyTimer = maxEnergyRegenTime;
            }
            #endregion
        }
        private void OnTriggerEnter(Collider player)
        {
            if (player.gameObject.tag == "Blade" || player.gameObject.tag == "safe")
            {
                curHealth = curHealth - 100;
            }
        }
        /*private void OnCollisionEnter(Collision player)
        {
            if(player.gameObject.tag == "SwarmBot" && !invincible)
            {
                curHealth -= 25;
            }
            return;
        }*/
        void GameOver()
        {
            if (curHealth <= 0)
            {
                SceneManager.LoadScene("Prototype");
            }
        }
        public void WaitForEnergyRegen()
        {
            //energyTimer = maxEnergyRegenTime;
            energyTimer += maxEnergyRegenTime;
            waitForRegen = true;
        }
        public void DamageBySwarmBot()
        {
            curHealth -= 25;
        }
    }
}