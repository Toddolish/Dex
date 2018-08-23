using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    Rigidbody rb;
    public float burstSpeed;
    public float forceSpeed;
    public float rotateSpeed;
    public Transform target;
    Weapon wep;

    public bool seeking;
    float timer;
    public float SeekLength;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wep = GameObject.Find("Lazer").GetComponent<Weapon>();
    }
    
    void FixedUpdate()
    {
        SeekPlayer();
    }

    void SeekPlayer()
    {
        if(seeking)
        {
            transform.LookAt(target);
            rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
            target = target.transform;
            timer += Time.deltaTime;
            if (timer > SeekLength)
            {
                seeking = false;
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Prototype");
        }
    }
}