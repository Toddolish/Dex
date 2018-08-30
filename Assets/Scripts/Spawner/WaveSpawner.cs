using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    { 
        SPAWNING, WAITING, COUNTING
    }
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves;
    private float waveCountdown;
    private SpawnState state = SpawnState.COUNTING;

    private float searchCountdown = 1;

	void Start ()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn Points Referenced");
        }

        waveCountdown = timeBetweenWaves;
	}
	
	void Update ()
    {
        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                //begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

		if(waveCountdown <= 0)
        {
           if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
	}
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("all waves completed");
        }
        else
        {
            nextWave++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
   IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("spawning wave" + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/ _wave.rate); //wait for the amount of seconds
        }

        state = SpawnState.WAITING;

        yield break; //end with break
    }
    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("spawning enemy" + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
