using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [Header("Lazer")]
    [Range(0.1f, 2)]
    public float timeBetweenShots = 0.5f;
    public float range = 100f;

    [SerializeField] float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;

    LineRenderer gunLine;

    public float effectsDisplayTime = 0.2f;
    public bool startCooldown = false;
    public Text coolingText;

    [Header("Materials")]
    public Material green;
    public Material red;

    Interactable enemyScript;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        coolingText = GameObject.Find("Timer").GetComponent<Text>();
        enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Interactable>();
    }
    void Update ()
    {
        coolingText.text = timer.ToString("F1");
        if (startCooldown)
        {
            gunLine.material = green;
            timer += Time.deltaTime;
            if(timer >= timeBetweenShots)
            {
                gunLine.material = red;
                startCooldown = false;
                timer = 0f;
            }
        }
        
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        if (startCooldown == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                startCooldown = true;
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        print("shot");
        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //if(shootHit.collider == "Enemy")
            //{
                enemyScript.SeekPlayer();
            //}
        }
    }

}
