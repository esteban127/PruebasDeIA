using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeNaves : MonoBehaviour {

    [SerializeField] int poblationNum;
    List<NeuronalNetwork> NeuronalList;
    int generation = 0;
    [SerializeField] int[] neuronalNetworkSize;
    [SerializeField]GameObject goal;
    [SerializeField] float generationLifeSpan;
    [SerializeField] GameObject shipPrefab;
    GameObject[] poblation;
    float counter = 0;

    void Start()
    {
        poblation = new GameObject[poblationNum];
        NeuronalList = new List<NeuronalNetwork>();
        for (int i = 0; i < poblationNum; i++)
        {
            
            GameObject myShip = Instantiate(shipPrefab);
            NeuronalNetwork myIA = new NeuronalNetwork(neuronalNetworkSize);
            NeuronalList.Add(myIA);
            myShip.GetComponent<ShipMovement>().ReciveNeuronalNetwork(myIA);
            myShip.GetComponent<ShipMovement>().SetGoal(goal.transform);
            myShip.transform.position = transform.position;
            poblation[i] = myShip;
        }
    }

    private void Update()
    {
        if(counter > generationLifeSpan)
        {
            NewGeneration();            
        }

        counter += Time.deltaTime;
    }

    private void NewGeneration()
    {
        for (int i = 0; i < poblationNum; i++)
        {
            poblation[i].GetComponent<ShipMovement>().CalculateFitness();
        }
        NeuronalList.Sort();        
    }
}
