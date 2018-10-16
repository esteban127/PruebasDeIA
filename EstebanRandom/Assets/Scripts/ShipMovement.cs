using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipMovement : MonoBehaviour {

    public float speed = 50;
    public float rotationCoef = 18;
    NeuronalNetwork myNeuronalNetwork;
    Transform goal;
    float[] goalObjetives;
    float[] actions;

    public void ReciveNeuronalNetwork(NeuronalNetwork neuNet)
    {
        myNeuronalNetwork = neuNet;
    }

    public void MoveForeward(float movement)
    {
        transform.position += movement* speed * transform.forward * Time.deltaTime;
    }

    public void MoveUp(float movement)
    {
        transform.position += movement * speed * transform.up * Time.deltaTime;
    }

    public void RotateShip (float rotation)
    {
        transform.Rotate(0, rotation * rotationCoef, 0, Space.Self);
    }
    
    public void CalculateFitness()
    {
        myNeuronalNetwork.SetFitness(Vector3.Magnitude(transform.position - goal.transform.position));
    }
    public void SetGoal(Transform goalTransform)
    {
        goal = goalTransform;
    }
    void SetObjetives()
    {
        goalObjetives = new float[3];
        goalObjetives[0] = Vector3.Angle(transform.forward, transform.position - goal.position);
        goalObjetives[1] = Vector3.Magnitude(new Vector3(transform.position.x,0,transform.position.z) - new Vector3(goal.position.x, 0, goal.position.z));
        goalObjetives[2] = transform.position.y - goal.position.y;
    }

    void FixedUpdate()
    {
        SetObjetives();
        actions = myNeuronalNetwork.FitFoward(goalObjetives);
        RotateShip(actions[0]);
        MoveForeward(actions[1]);
        MoveUp(actions[2]);
    }      

}
