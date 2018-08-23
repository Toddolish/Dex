using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public NavMeshAgent agent;
    Rigidbody myrb;
    public float burstSpeed;
    public float rotateSpeed;
    public Transform target;

    //suck
    //once object seeking is true
    //object will move quickly towards player target with a timer of 1 sec
    //then it will loose target but keep moving forward
    //and steering speed will be 0

    public bool seeking;
    float timer;
    public float SeekLength;

    void Start()
    {
        myrb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        SeekPlayer();
        agent.SetDestination(target.position);
        if(Input.GetKeyDown(KeyCode.E))
        {
            seeking = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Player")
       {
          SceneManager.LoadScene("Prototype");
       }
    }
    void SeekPlayer()
    {
        if(seeking)
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
            timer += Time.deltaTime;
            agent.speed = burstSpeed;
            if (timer > SeekLength)
            {
                target = null;
                seeking = false;
                timer = 0;
            }
        }
    }
}