using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotate : MonoBehaviour
{
    public float rotateSpeed;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        this.gameObject.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
	}
}
