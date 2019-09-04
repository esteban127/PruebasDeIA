using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockGenerator : MonoBehaviour
{
    [SerializeField] GameObject leader;
    [SerializeField] GameObject followersPrefab;
    [SerializeField] Vector3 SpawnPoint;
    [SerializeField] int boidsAmmount;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    Flocking[] christians;
    private void Start()
    {
        FoundChurch();        
    }

    private void FoundChurch()
    {
        GameObject follower;
        Vector3 rand;
        Vector3 speed;
        FlockingLeader jesus = leader.GetComponent<FlockingLeader>();
        christians = new Flocking[boidsAmmount];
        for (int i = 0; i < boidsAmmount; i++)
        {
            rand = SpawnPoint + Random.insideUnitSphere * 30;
            follower = Instantiate(followersPrefab);
            follower.transform.position = rand;
            speed = new Vector3(Random.Range(0, maxSpeed), Random.Range(0, maxSpeed), Random.Range(0, maxSpeed));
            speed = Flocking.ClampSpeed(speed, minSpeed, maxSpeed);
            Flocking jose = follower.GetComponent<Flocking>();
            jose.Set(speed, jesus, minSpeed, maxSpeed);
            christians[i] = jose;
        }
        speed = new Vector3(Random.Range(0, maxSpeed), Random.Range(0, maxSpeed), Random.Range(0, maxSpeed));
        speed = Flocking.ClampSpeed(speed, minSpeed, maxSpeed);
        jesus.Set(christians, speed,minSpeed,maxSpeed);
    }
}
