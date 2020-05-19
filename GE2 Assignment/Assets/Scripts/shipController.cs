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
                target = enemyShips[Random.Range(0, enemyShips.Length)];
                break;
            case "redTeamShip":
                enemyShips = GameObject.FindGameObjectsWithTag("blueTeamShip");
                target = enemyShips[Random.Range(0, enemyShips.Length)];
                break;
        }
        
        owner.GetComponent<Arrive>().targetGameObject = target;
        owner.GetComponent<Arrive>().enabled = true;
        owner.GetComponent<shipController>().targetEnemy = target;
    }
    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<shipController>().targetEnemy.transform.position,
            owner.transform.position) < 1000)
        {
            owner.ChangeState(new PursueState());
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

    public override void Think()
    {
        if (owner.GetComponent<shipController>().hpPercent <= 20)
        {
            owner.ChangeState(new FleeState());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<OffsetPursue>().enabled = false;
    }
}

class FleeState : State
{
    private float time;
    public override void Enter()
    {
        owner.GetComponent<Arrive>().targetGameObject = owner.transform.parent.gameObject;
        owner.GetComponent<squadController>().targetEnemy = owner.transform.parent.gameObject;
        owner.GetComponent<Boid>().maxSpeed = owner.GetComponent<squadController>().tempSpeed;
        owner.GetComponent<Arrive>().enabled = true;
    }
    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<squadController>().targetEnemy.transform.position,
            owner.transform.position) < 20)
        {
            owner.ChangeState(new FindTargetState());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = true;
    }
}

public class shipController : MonoBehaviour
{
    public GameObject targetEnemy;
    public float maxHP;
    private float currentHP;
    public float hpPercent;
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
    }
    
}
