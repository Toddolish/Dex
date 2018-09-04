using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeBot_Spawner : MonoBehaviour
{
    public float minSpawnTime;
    public float maxSpawnTime;
    public float spawnTime;
    public GameObject memeBot;


    // Use this for initialization
    void Start()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnTime <= 0)
        {
            Instantiate(memeBot,transform);
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
        spawnTime = spawnTime -1* Time.deltaTime;

    }
}
