﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NeuronalNetwork : IComparable<NeuronalNetwork> {


    int[] layers;
    int[] Layers { get { return layers; } }
    float[][] neuronas;
    float[][][] pesos;
    float[][][] Pesos { get { return pesos; } }
    float fitness;
    public float Fitness {get{return fitness;}}
    public void SetFitness(float value) { fitness = value; }
    float[] bias;
    public float[] Bias { get { return bias; } }

    public NeuronalNetwork( int[] initilLayers)
    {
        layers = copyArray(initilLayers);
        InicializarBias(layers);
        InicializarNeuronas(layers);
        InicializarPesos(layers);
    }

    

    public NeuronalNetwork(NeuronalNetwork parent)
    {

        layers = parent.Layers;
        bias = copyArray(parent.Bias); 
        InicializarNeuronas(layers);
        CopiarPesos(parent.Pesos);

    }

    private void InicializarBias(int[] layers)
    {
        bias = new float[layers.Length - 2];
        for (int i = 0; i < bias.Length; i++)
        {
            bias[i] = UnityEngine.Random.Range(-50.0f, 50.0f);
        }
    }

    private int[] copyArray(int[] array)
    {
        int[] myArray = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            myArray[i] = array[i];
        }
        return myArray;
    }
    private float[] copyArray(float[] array)
    {
        float[] myArray = new float[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            myArray[i] = array[i];
        }
        return myArray;
    }



    private void CopiarPesos(float[][][] parentPeso)
    {
        pesos = new float[parentPeso.Length][][];
        List<float> provitionalList;
        List<float[]> provitionalArrayList;
        for (int i = 0; i < parentPeso.Length; i++)
        {
            provitionalArrayList = new List<float[]>();
            for (int j = 0; j < parentPeso[i].Length; j++)
            {                
                provitionalList = new List<float>();
                for (int k = 0; k < parentPeso[i][j].Length; k++)
                {
                    provitionalList.Add(parentPeso[i][j][k]);
                }
                provitionalArrayList.Add(provitionalList.ToArray());
            }
            pesos[i] = provitionalArrayList.ToArray();
        }
    }

    public float[] FitFoward(float[] input)
    {
        float neuronaActual = 0;
        int biasCount = 1;
        if (input.Length == layers[0]) 
        {
            neuronas[0] = input;
            for (int i = 1; i < layers.Length; i++)
            {

                if (i == layers.Length-1)
                    biasCount = 0;

               
                for (int j = 0; j < layers[i] - biasCount; j++)
                {
                    
                    neuronaActual = 0;                    
                    for (int k = 0; k < pesos[i-1][j].Length; k++)
                    {
                        
                        neuronaActual += (pesos[i-1][j][k] * neuronas[i-1][k]);
                        
                    }                    
                    neuronas[i][j] = neuronaActual;
                }
                

            }           
            return neuronas[neuronas.Length-1];
        }
        else
        {
            Debug.LogError("Tamaño de input no coincide");
            return new float[0];
        }
        
    }

    public void Mutar()
    {       
        float value = 0;
        int random;
        for (int i = 0; i < pesos.Length; i++)
        {
            for (int j = 0; j < pesos[i].Length; j++)
            {
                for (int k = 0; k < pesos[i][j].Length; k++)
                {
                    value = pesos[i][j][k];
                    random = (int)UnityEngine.Random.Range(0, 100);

                    switch (random)
                        {

                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                            value *= -1;
                            break;

                        case 27:
                        case 28:
                            value -= 0.1f;
                            break;
                        case 29:
                        case 30:
                            value *= 2;
                            break;

                        case 72:
                        case 73:
                            value += 0.1f;
                            break;
                        case 74:
                        case 75:
                            value *= 0.5f;
                            break;
                        }
                    pesos[i][j][k] = value;

                }
            }
            if (i < 0 && i < pesos.Length - 1)
                bias[i] = value + (bias[i] * value);
        }
        InicializarNeuronas(layers);
    }




    void InicializarNeuronas(int[] neuronalLayer)
    {
        List<float> provitionalList;
        neuronas = new float[neuronalLayer.Length][];
        for (int i = 0; i < neuronalLayer.Length; i++)
        {
            provitionalList = new List<float>();
            for (int j = 0; j < neuronalLayer[i]; j++)
            {
                
                if (i > 0 && i < neuronalLayer.Length - 1 && j == neuronalLayer[i] - 1)
                {
                    provitionalList.Add(bias[i-1]);
                }
                else
                {
                    provitionalList.Add(0.0f);
                }
            }
            neuronas[i] = provitionalList.ToArray();
        }
        
    }

    void InicializarPesos(int[] neuronalLayer)
    {
       
        List<float> provitionalList;
        List<float[]> provitionalArrayList;
        pesos = new float[neuronalLayer.Length - 1][][];
        float value;
        for (int i = 0; i < neuronalLayer.Length - 1; i++)
        {
            provitionalArrayList = new List<float[]>();
            for (int j = 0; j < neuronalLayer[i+1]; j++)
            {
                
                provitionalList = new List<float>();
                for (int k = 0; k < neuronalLayer[i]; k++)
                {
                    value = UnityEngine.Random.Range(-0.5f, 0.5f);
                    provitionalList.Add(value);
                }
                provitionalArrayList.Add(provitionalList.ToArray());
            }
            pesos[i] = provitionalArrayList.ToArray();
        }

    }

    public int CompareTo(NeuronalNetwork other)
    {
        if (fitness == other.Fitness)
            return 0;
        if (fitness < other.Fitness)
            return 1;

        return -1;
    }
}
