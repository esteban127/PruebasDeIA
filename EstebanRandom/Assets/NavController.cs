using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavController : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    NavMeshAgent agent;
    int currentTarget = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targets[0].position);
    }
    private void Update()
    {
        if ((transform.position - agent.destination).magnitude <= 1)
        {
            currentTarget++;
            currentTarget = currentTarget % targets.Length;
            agent.SetDestination(targets[currentTarget].position);
        }
    }
}
