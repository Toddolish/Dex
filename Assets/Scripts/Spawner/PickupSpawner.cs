using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Information")]
    public Transform spawnPoint;
    public GameObject pickup;

    [Header("Spawner Values")]
    [SerializeField] float spawnTime;
    public float timer;
    public bool SlotFull = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            Pickup();
            timer = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Energy" || other.gameObject.tag == "Health" || other.gameObject.tag == "DarkDex")
        {
            SlotFull = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Energy" || other.gameObject.tag == "Health" || other.gameObject.tag == "DarkDex")
        {
            SlotFull = false;
        }
    }
    void Pickup()
    {
        if(!SlotFull)
        {
            Instantiate(pickup, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
