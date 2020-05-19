using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engage : MonoBehaviour
{
    public gunScript[] gunScripts;
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
        gunScripts = GetComponentsInChildren<gunScript>();
        foreach (gunScript gun in gunScripts)
        {
            gun.enabled = true;
            alreadyRan = false;
        }
    }
    private void disengageEnemy()
    {
        gunScripts = GetComponentsInChildren<gunScript>();
        foreach (gunScript gun in gunScripts)
        {
            gun.enabled = false;
        }
        alreadyRan = true;
    }

}
