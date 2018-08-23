using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    Interactable enemyInteract;
    public Transform Trans;
    public bool inside;
    public GameObject movableTarget;

    //timer
    float timer;
    public Text coolingText;

	void Start ()
    {
        enemyInteract = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Interactable>();
        Trans = GameObject.Find("trans").GetComponent<Transform>();
        coolingText = GameObject.Find("Timer").GetComponent<Text>();
    }
	
	void Update ()
    {
        coolingText.text = timer.ToString("F1");
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = 0;
        }
        if (timer < 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                inside = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                inside = false;
                timer = 2;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && inside)
        {
            enemyInteract.seeking = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && inside)
        {
            enemyInteract.seeking = true;
        }
    }
}
