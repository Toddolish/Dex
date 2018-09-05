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
    public float min;
    public float max;
    public bool spawn;

    [Header("Is this a Health spawner or an Energy spawner.")]
    public bool energySpawner;

    void Start()
    {
    }
    
    void Update()
    {
        spawnTime = Random.Range(min, max);
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
    void Energy()
    {
        Instantiate(EnergyPickup, spawnPoint.position, spawnPoint.rotation);
        spawnTime = Random.Range(min, max);
    }
    void Health()
    {
        Instantiate(HealthPickup, spawnPoint.position, spawnPoint.rotation);
        spawnTime = Random.Range(min, max);
    }
}
