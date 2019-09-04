using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    FlockingLeader leader;
    Vector3 actualSpeed;
    public Vector3 ActualSpeed { get { return actualSpeed; } }
    float maxSpeed;
    float minSpeed;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Set(Vector3 speed, FlockingLeader Nleader, float NminSpeed, float NmaxSpeed)
    {
        actualSpeed = speed;
        minSpeed = NminSpeed;
        maxSpeed = NmaxSpeed;
        leader = Nleader;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public static Vector3 ClampSpeed(Vector3 speed, float min, float max)
    {
        if(speed.magnitude<min )
        {
            return speed.normalized * min;
        }
        if(speed.magnitude > max)
        {
            return speed.normalized * max;
        }
        
        return speed;
        
    }

    private void FixedUpdate()
    {
        Vector3 randomSpeed;
        randomSpeed = new Vector3(Random.Range(0, maxSpeed), Random.Range(0, maxSpeed), Random.Range(0, maxSpeed));
        randomSpeed += leader.SpeedOfTheGroup;
        randomSpeed += ( leader.CenterOfTheGroup - transform.position )/10;
        randomSpeed += (leader.GetLeaderPos() - transform.position) /2;
        rb.velocity = ClampSpeed(randomSpeed / 4, minSpeed, maxSpeed);
        actualSpeed = rb.velocity;
    }
}
