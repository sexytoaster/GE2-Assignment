using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engage : MonoBehaviour
{
    public gunScript[] gunScripts;
    public bombScript[] bombScripts;

    public GameObject leader;
    public Arrive leaderArrive;
    private bool alreadyRan = false;

    void Start()
    {
        leaderArrive = leader.GetComponent<Arrive>();
        
    }

    void Update()
    {
        if(leaderArrive.enabled == false)
        {
            engageEnemy();
        }
        if (leaderArrive.enabled == true && alreadyRan == false)
        {
            disengageEnemy();
        }
    }

    private void engageEnemy()
    {
        bombScripts = GetComponentsInChildren<bombScript>();
        gunScripts = GetComponentsInChildren<gunScript>();
        foreach (gunScript gun in gunScripts)
        {
            gun.enabled = true;
            
        }
        foreach (bombScript bomb in bombScripts)
        {
            bomb.enabled = true;
            
        }
        alreadyRan = false;
    }
    private void disengageEnemy()
    {
        bombScripts = GetComponentsInChildren<bombScript>();
        gunScripts = GetComponentsInChildren<gunScript>();
        foreach (gunScript gun in gunScripts)
        {
            gun.enabled = false;
        }
        foreach (bombScript bomb in bombScripts)
        {
            bomb.enabled = false;

        }
        alreadyRan = true;
    }

}
