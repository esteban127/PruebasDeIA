using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeNaves : MonoBehaviour {

    [SerializeField] int poblationNum;
    List<NeuronalNetwork> NeuronalList;
    public int generation = 0;
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
        poblation[0].name = "Pro";
        poblation[0].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.cyan;
        poblation[0].transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
        poblation[0].transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.yellow;
        poblation[0].transform.GetChild(3).GetComponent<MeshRenderer>().material.color = Color.yellow;

    }

    private void Update()
    {
        if(counter > generationLifeSpan)
        {
            NewGeneration();
            counter = 0;
        }

        counter += Time.deltaTime;
    }

    private void NewGeneration()
    {
        generation++;
        for (int i = 0; i < poblationNum; i++)
        {
            poblation[i].GetComponent<ShipMovement>().CalculateFitness();
        }
        NeuronalList.Sort();
        for (int i = 0; i < poblationNum / 2; i++)
        {
            NeuronalList[poblationNum - 1 - i] = new NeuronalNetwork(NeuronalList[i]);
            NeuronalList[i] = new NeuronalNetwork(NeuronalList[i]);
            NeuronalList[poblationNum - 1 - i].Mutar();
            
        }
        for (int i = 0; i < poblationNum; i++)
        {
            poblation[i].GetComponent<ShipMovement>().ReciveNeuronalNetwork(NeuronalList[i]);        
            poblation[i].transform.position = transform.position;
            poblation[i].transform.rotation = transform.rotation;
        }        
    }
}
