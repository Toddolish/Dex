using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    Interactable enemyInteract;
    public bool inside;
	void Start ()
    {
        enemyInteract = GameObject.Find("DangerBox").GetComponent<Interactable>();
    }
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            inside = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            inside = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && inside)
        {
               Debug.Log("work");
               enemyInteract.seeking = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && inside)
        {
            Debug.Log("work");
            enemyInteract.seeking = true;
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            inside = false;
            // Debug.Log("work");
            //enemyInteract.seeking = true;
        }
    }*/
}
