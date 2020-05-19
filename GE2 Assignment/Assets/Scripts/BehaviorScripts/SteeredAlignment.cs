using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredAlignment")]
public class SteeredAlignment : FlockBehaviour
{

    Vector3 currentVelocity;
    public float agentSmoothTime = 1f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, maintain alignment
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }
        //add all points together and average
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in context)
        {
            alignmentMove += item.transform.forward;
            alignmentMove = Vector3.SmoothDamp(agent.transform.forward, alignmentMove, ref currentVelocity, agentSmoothTime);
        }
        alignmentMove /= context.Count;


        return alignmentMove;
    }
}