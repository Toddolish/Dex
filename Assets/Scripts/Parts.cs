using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    ScoreScript scoreScript;

    Rigidbody rb;
    public float forceSpeed;
    public Transform target;
    bool seekTime;

    [Header("WORTH")]
    [Header("The value of which the score in increased")]
    public int value;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").GetComponent<Transform>();
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
        if(other.gameObject.tag == "Player")
        {
            //find the reference to the score
            scoreScript = GameObject.Find("EventManager").GetComponent<ScoreScript>();

            //plus 50 points
            scoreScript.scoreCount += value;

            //delete this Object
            Destroy(this.gameObject);
        }
    }
    public void SeekPlayer()
    {
        seekTime = true;
    }
}
