using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engage : MonoBehaviour
{
    public bool engageEnemy = true;
    private void OnCollisionEnter(Collision collision)
    {
        if(this.tag == "blueTeam" && collision.transform.tag == "redTeam")
        {
            engageEnemy = true;
        }
        if (this.tag == "redTeam" && collision.transform.tag == "blueTeam")
        {
            engageEnemy = true;
        }
    }
}
