using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Information")]
    public Transform spawnPoint;
    public GameObject HealthPickup;
    public GameObject EnergyPickup;

    [Header("Spawner Values")]
    [SerializeField]float spawnTime;
    public float timer;
    public bool SlotFull = false;

    [Header("Is this a Health spawner or an Energy spawner.")]
    public bool energySpawner;

    void Start()
    {

    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (energySpawner)
        {
            if (timer > spawnTime)
            {
                Energy();
                timer = 0;
            }
        }
        if (!energySpawner)
        {
            if (timer > spawnTime)
            {
                Health();
                timer = 0;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" || other.gameObject.tag == "Health")
        {
            SlotFull = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Health" || other.gameObject.tag == "Health")
        {
            SlotFull = false;
        }
    }
    void Energy()
    {
        if(!SlotFull)
        {
            Instantiate(EnergyPickup, spawnPoint.position, spawnPoint.rotation);
        }
    }
    void Health()
    {
        if(!SlotFull)
        {
            Instantiate(HealthPickup, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
