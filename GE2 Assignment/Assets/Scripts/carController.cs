using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class FindTargetState : State
{
    Vector3 targetPos;

    public override void Enter()
    {
        GameObject[] trafficLights = GameObject.FindGameObjectsWithTag("Green");

        GameObject target = trafficLights[Random.Range(0, trafficLights.Length)];

        owner.GetComponent<Arrive>().targetGameObject = target;
        owner.GetComponent<Arrive>().enabled = true;
        owner.GetComponent<carController>().targetTrafficLight = target;
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}
public class carController : MonoBehaviour
{
    public GameObject targetTrafficLight;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new FindTargetState());
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTrafficLight == null)
        {
            GetComponent<StateMachine>().ChangeState(new FindTargetState());
        }
        if(targetTrafficLight.tag != "Green" || Vector3.Distance(targetTrafficLight.transform.position, this.transform.position) <= 1)
        {
            GetComponent<StateMachine>().ChangeState(new FindTargetState());
        }
    }
    
}
