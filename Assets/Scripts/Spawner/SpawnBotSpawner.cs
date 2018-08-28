using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBotSpawner : MonoBehaviour
{
    [Header("BOTS")]
    public GameObject SwarmBot;
    public GameObject GyroBot;

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
        if (timer > 10 && timer < 11)
        {
            SpawnGyroBot();
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

    }
    void SpawnSwarmBot()
    {
        Instantiate(SwarmBot, spawner.position, spawner.rotation);
        timer = 0;
    }
    void SpawnGyroBot()
    {
        Instantiate(GyroBot, spawner.position, spawner.rotation);
        timer = 0;
    }
}
