using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behavior;

    public int spawnRadius = 20;

    [Range(10, 50)]
    public int startingCount = 10;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for(int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, new Vector3(transform.position.x + Random.Range(-spawnRadius, spawnRadius), transform.position.y, transform.position.z + Random.Range(-spawnRadius, spawnRadius)), Quaternion.Euler(Vector3.forward), transform);
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);

            }
        }

        return context;
    }
}
