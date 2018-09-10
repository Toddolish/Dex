using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        #region Health
        [Header("HEALTH")]
        public float curHealth;
        public float maxHealth;
        public float xHealth;
        public float xReductionSpeed;
        Image xHealthBar;
        Image hpBar;
        #endregion
        #region   Energy
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
        #endregion
        #region DarkDex
        [Header("DarkDex")]
        public float curDarkDex;
        public float maxDarkDex;
        public float xDarkDex; //the health behind the health to show indication of how much health you lost
        public float darkDexRegenSpeed; //the speed of the regeneration
        public float maxDarkDexRegenTime; //how long it takes after dash before start regeneration
        public float darkDexTimerSpeed;
        public float darkDexTimer;
        Image xDarkDexBar;
        Image darkDexBar;
#endregion

        public bool invincible;
        PlayerMovement playerMoveScript;
        GyroBot gyroBot;
        Parts partsPickup;

        #region Raycast
        [Header("RAYCAST")]
        public float range = 5f;
        public float radius = 5f;
        Ray shootRay;
        RaycastHit shootHit;
        public LayerMask shootableMask;
#endregion


        void Start()
        {
            playerMoveScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
            gyroBot = GameObject.Find("Player").GetComponent<GyroBot>();
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
            
            #region Health
            hpBar.fillAmount = (curHealth / 100);
            xHealthBar.fillAmount = (xHealth / 100);
            if(curHealth > maxHealth)
            {
                curHealth = maxHealth;
            }
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
            Invincible();
            Magnet();
        }
        private void OnTriggerEnter(Collider player)
        {
            if (player.gameObject.tag == "Blade" || player.gameObject.tag == "safe" || player.gameObject.tag == "Danger") //when gyrobot is hacked he goes into blade mode else he is in safe mode, still dangerous only for player
            {
                DamageByGyro();
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
            //Gizmos.DrawRay();
        }
        void Magnet()
        {
            //All Pickups within range of Radius will activate their own seek Fuction. 
            //|<-Definition->|<-------------------------Declaration------------------------->
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, shootableMask);

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    Debug.Log(hits[i].name); //to check what i am hitting
                    Parts partsPickup = hits[i].GetComponent<Parts>();
                    if (partsPickup)
                    {
                        partsPickup.SeekPlayer();
                    }
                }
            }
        }
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
        public void DamageByGyro()
        {
            curHealth -= 50;
            //invincible = true;
        }
        public void DamageByMeme()
        {
            curHealth -= 25;
        }
        public void Invincible()
        {
            if (playerMoveScript.dashing)
            {
                invincible = true;
            }
            else
            {
                invincible = false;
            }
        }
    }
}