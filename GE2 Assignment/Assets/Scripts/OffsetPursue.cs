
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour
{
    public Boid targetShip;
    Vector3 targetPos;
    Vector3 worldTarget;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targetShip.transform.position;

        offset = Quaternion.Inverse(targetShip.transform.rotation) * offset;
    }

    public override Vector3 Calculate()
    {
        worldTarget = targetShip.transform.TransformPoint(offset);
        float dist = Vector3.Distance(worldTarget, transform.position);
        float time = dist / boid.maxSpeed;
        targetPos = worldTarget + (targetShip.velocity * time);
        force = boid.ArriveForce(targetPos);
        return force;

    }
}
