using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("VARIABLES")]
    public float movementSpeed;
	void Start ()
    {

    }
	
	void Update ()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        this.gameObject.transform.Translate(hor * movementSpeed, 0, ver * movementSpeed);

    }
}
