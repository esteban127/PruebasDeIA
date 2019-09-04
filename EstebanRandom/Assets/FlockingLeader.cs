using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingLeader : MonoBehaviour
{
    Flocking[] party;
    Vector3 currentSpeed;
    Vector3 centerOfTheGroup;
    public Vector3 CenterOfTheGroup { get { return centerOfTheGroup; } }
    Vector3 speedOfTheGroup;
    public Vector3 SpeedOfTheGroup { get { return speedOfTheGroup; } }
    float minSpeed;
    float maxSpeed;

    Rigidbody rb;
    [SerializeField] GameObject route;
    int currentobjetive = 0;

    Vector3 leaderSpeed;

    public void Set(Flocking[] newParty, Vector3 speed, float nMinSpeed, float nMaxSpeed)
    {
        rb = GetComponent<Rigidbody>();
        party = newParty;
        currentSpeed = speed;
        minSpeed = nMinSpeed;
        maxSpeed = nMaxSpeed;
    }
    private void FixedUpdate()
    {
        MoveToTheNextPoint();
        CalculateGroupStats();
    }

    private void CalculateGroupStats()
    {
        Vector3 groupPos = transform.position;
        Vector3 groupSpeed = rb.velocity;
        for (int i = 0; i < party.Length; i++)
        {
            groupPos += party[i].GetPos();
            groupSpeed += party[i].ActualSpeed;
        }
        centerOfTheGroup = groupPos / (party.Length + 1);
        speedOfTheGroup = groupSpeed / (party.Length + 1);
    }

    private void MoveToTheNextPoint()
    {
        rb.velocity = currentSpeed/2;
    }

    public Vector3 GetLeaderPos()
    {
        return transform.position;
    }

}
