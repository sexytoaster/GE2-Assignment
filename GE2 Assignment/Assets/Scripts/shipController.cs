using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class TargetState : State
{
    Vector3 targetPos;
    GameObject[] enemyShips;
    GameObject target;
    public override void Enter()
    {
        switch (owner.tag)
        {
            case "blueTeamShip":
                enemyShips = GameObject.FindGameObjectsWithTag("redTeamShip");
                if(enemyShips.Length != 0)
                {
                    target = enemyShips[Random.Range(0, enemyShips.Length)];
                }
                break;
            case "redTeamShip":
                enemyShips = GameObject.FindGameObjectsWithTag("blueTeamShip");
                if(enemyShips.Length != 0)
                {
                    target = enemyShips[Random.Range(0, enemyShips.Length)];
                }
                
                break;
        }
        
        owner.GetComponent<Arrive>().targetGameObject = target;
        owner.GetComponent<Arrive>().enabled = true;
        owner.GetComponent<shipController>().targetEnemy = target;
    }
    public override void Think()
    {
        if(owner.GetComponent<shipController>().targetEnemy != null)
        {
            if (Vector3.Distance(
            owner.GetComponent<shipController>().targetEnemy.transform.position,
            owner.transform.position) < 1000)
            {
                owner.ChangeState(new PursueState());
            }
        }
        
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

class PursueState : State
{
    public override void Enter()
    { 
        owner.GetComponent<OffsetPursue>().enabled = true;
        owner.GetComponent<OffsetPursue>().targetShip = owner.GetComponent<shipController>().targetEnemy.GetComponent<Boid>();
    }
}


public class shipController : MonoBehaviour
{
    public GameObject targetEnemy;
    public float maxHP;
    private float currentHP;
    public float hpPercent;
    public float bigLaserDamage = 5;
    public float smallLaserDamage = 1;
    private bool damaged = false;
    public GameObject explosionEffect;
    public GameObject plasmaEffect;
    public GameObject smokeEffect;
    public GameObject fireEffect;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        hpPercent = (currentHP / maxHP) * 100;
        GetComponent<StateMachine>().ChangeState(new TargetState());
    }

    // Update is called once per frame
    void Update()
    {
        hpPercent = (currentHP / maxHP) * 100;
        if (targetEnemy == null)
        {
            GetComponent<StateMachine>().ChangeState(new TargetState());
        }
        if (currentHP <= 30 && damaged == false)
        {
            Damage();
        }
        if (currentHP <= 0)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "smallLaser")
        {
            currentHP -= smallLaserDamage;
        }
        if (collision.gameObject.tag == "bigLaser")
        {
            currentHP -= bigLaserDamage;
        }
    }

    public void Damage()
    {
        GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.Euler(new Vector3(-90, -180,180)));
        GameObject fire = Instantiate(fireEffect, transform.position, transform.rotation);
        smoke.transform.parent = gameObject.transform;
        fire.transform.parent = gameObject.transform;
        damaged = true;
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Instantiate(plasmaEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
