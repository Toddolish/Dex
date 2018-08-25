using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBotSpawner : MonoBehaviour
{
    [Header("BOTS")]
    public GameObject SwarmBot;

    [Header("SPAWNERS")]
    public Transform spawner;
    public int spawnerIndex;

    [Header("VARIABLES")]
    public float spawnTime;
    public float minSpawnTime;
    public float maxSpawnTime;
    float timer;

	void Start ()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
	
	void Update ()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            SpawnSwarmBot();
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

    }
    void SpawnSwarmBot()
    {
        Instantiate(SwarmBot, spawner.position, spawner.rotation);
        timer = 0;
    }
}
