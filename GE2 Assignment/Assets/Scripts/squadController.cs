using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class FindTargetState : State
{
    Vector3 targetPos;

    public override void Enter()
    {
        GameObject[] enemyShips = GameObject.FindGameObjectsWithTag("redTeam");

        GameObject target = enemyShips[Random.Range(0, enemyShips.Length)];

        owner.GetComponent<Arrive>().targetGameObject = target;
        owner.GetComponent<Arrive>().enabled = true;
        owner.GetComponent<squadController>().targetEnemy = target;
        owner.GetComponent<squadController>().tempSpeed = owner.GetComponent<Boid>().maxSpeed;
    }
    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<squadController>().targetEnemy.transform.position,
            owner.transform.position) < 100)
        {
            owner.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}
class AttackState : State
{
    private float time;
    public override void Enter()
    {
        owner.GetComponent<Boid>().maxSpeed = 0;
        time = Time.time;
    }
    public override void Think()
    {
        if (Time.time - time >= 20)
        {
            owner.ChangeState(new RefuelState());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = true;
    }
}

class RefuelState : State
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
public class squadController : MonoBehaviour
{
    public GameObject targetEnemy;
    public float tempSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new FindTargetState());
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy == null)
        {
            GetComponent<StateMachine>().ChangeState(new FindTargetState());
        }
    }
    
}
