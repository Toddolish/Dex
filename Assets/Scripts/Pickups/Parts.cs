using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parts : MonoBehaviour
{
    [Header("IMPORTANT VARIABLES")]
    ScoreScript scoreScript;
    Rigidbody rb;
    public float forceSpeed;
    public Transform target;
    bool seekTime;

    [Header("WORTH")]
    [Header("The value of which the score in increased")]
    public int value;
    public GameObject melted;
    float timer;

    [Header("MULTIPLIER")]
    public bool goldenPart;
    
    void Start()
    {
        scoreScript = GameObject.Find("EventManager").GetComponent<ScoreScript>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Expired();
        }
    }
    void FixedUpdate()
    {
        if (seekTime)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, forceSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        scoreScript = GameObject.Find("EventManager").GetComponent<ScoreScript>();
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            value *= scoreScript.multiplierCount;
            if (goldenPart == true)
            {
                scoreScript.multiplierCount++;
            }
            //plus 50 points
            scoreScript.scoreCount += value;

            //delete this Object
            Destroy(this.gameObject);
        }
    }
    public void SeekPlayer()
    {
        seekTime = true;
        timer = 0;
    }
    public void Expired()
    {
        Destroy(this.gameObject);
        Instantiate(melted, transform.position, transform.rotation);
        timer = 0;
    }
}
