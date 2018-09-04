using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Tutorial : MonoBehaviour

{
    [Header("ObjectiveCompletion")]
    public bool basicMovement;
    public bool useWeapon;
    private bool pull1;
    private bool pull2;
    public bool dash;

    [Header("Reference")]
    public PlayerStats statsScript;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if((pull1 = true) && (pull2 = true))
        {
            useWeapon = true;
        }
        #region Basic Movement

        #endregion

        #region Use Weapon
//spawn a swarm bot
// instantiate, pause game,show pull key,continue game when 
//spawn a health pack
    //pull to complete
        #endregion

        #region Dash
        //learn that dashing can kill swarm bot
        //spawn a circle of swarm bot around you
        //display dash key, freeze game until you pressed dash key

        #endregion
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BasicMovement")
        {
            basicMovement = true;
  
        }
        if (other.gameObject.name =="Pull1")
        {
            pull1 = true;
        }
        if (other.gameObject.name =="Pull2")
        {
            pull2 = true;
        }
    }
}
