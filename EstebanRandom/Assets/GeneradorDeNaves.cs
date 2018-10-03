using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeNaves : MonoBehaviour {

    [SerializeField] int poblationNum;
    List<NeuronalNetwork> NeuronalList;
    int generation = 0;
    [SerializeField] int[] neuronalNetworkSize;
    [SerializeField]GameObject goal;

    [SerializeField] GameObject shipPrefab;
    GameObject[] poblation;

    void Start()
    {
        poblation = new GameObject[poblationNum];

        for (int i = 0; i < poblationNum; i++)
        {
            GameObject myShip = Instantiate(shipPrefab);
            NeuronalNetwork myIA = new NeuronalNetwork(neuronalNetworkSize);
            NeuronalList.Add(myIA);
            myShip.GetComponent<ShipMovement>().ReciveNeuronalNetwork(myIA);
            myShip.transform.position = transform.position;
            poblation[i] = myShip;
        }
    }

}
