using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public Transform enemy2;
        public int count2;
    }
    public Text waveText;
    public int waveCount;
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves;
    private float waveCountdown;
    private SpawnState state = SpawnState.COUNTING;

    private float searchCountdown = 1;
    [Header("PROGRESSION")]
    [Header("BOTS")]
    public GameObject goldSwarmBot;
    public ParticleSystem dustStorm;
    //bools
    bool goldTime = false;
    bool goldTime2 = false;
    bool goldTime3 = false;
    bool dustTime = false;

    void Start ()
    {
        waveCount = 1;
        waveText = GameObject.Find("WaveCount").GetComponent<Text>();
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn Points Referenced");
        }
        waveCountdown = timeBetweenWaves;
    }
	
	void Update ()
    {
        ProgressionEvents();
        waveText.text = waveCount.ToString();
        if (state == SpawnState.WAITING)
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
            //after all enemys defeated
            //next wave
            waveCount++;
            nextWave++;
            //do something cool
        }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown < 3f)
        {
            searchCountdown = 0.1f;
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
         for (int i = 0; i < _wave.count2; i++)
        {
            SpawnEnemy2(_wave.enemy2);
            yield return new WaitForSeconds(1f/ _wave.rate); //wait for the amount of seconds
        }

        state = SpawnState.WAITING;

        yield break; //end with break
    }
    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("spawning enemy" + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
    void SpawnEnemy2(Transform _enemy2)
    {
        //Debug.Log("spawning enemy" + _enemy2.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy2, _sp.position, _sp.rotation);
    }
    public void ProgressionEvents()
    {
        if(goldTime == false)
        {
            if (waveCount == 5)
            {
                //dust storm
                //maybe even do something cool to the blade
                Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(goldSwarmBot, _sp.position, _sp.rotation);
                goldTime = true;
            }
        }
        if (dustTime == false)
        {
            if (waveCount == 7)
            {
                dustStorm.Play();
            }
        }
        if (goldTime2 == false)
        {
            if (waveCount == 10)
            {
                //dust storm
                //maybe even do something cool to the blade
                Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(goldSwarmBot, _sp.position, _sp.rotation);
                goldTime2 = true;
            }
        }
        if (goldTime3 == false)
        {
            if (waveCount == 15)
            {
                //dust storm
                //maybe even do something cool to the blade
                Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(goldSwarmBot, _sp.position, _sp.rotation);
                goldTime3 = true;
            }
        }
    }
}
