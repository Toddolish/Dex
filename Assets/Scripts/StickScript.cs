using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickScript : MonoBehaviour
{
    public Transform target;
    Transform myTrans;
    void Start()
    {
        myTrans = GetComponent<Transform>();
        target = GetComponent<Transform>();
    }
    
    void Update()
    {
        myTrans.position = target.transform.position;
    }
}
