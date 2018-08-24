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

    public bool seeking;
    float timer;
    public float SeekLength;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
    }

    public void SeekPlayer()
    {
            transform.LookAt(target);
            rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
            target = target.transform;
            timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Prototype");
        }
    }
}