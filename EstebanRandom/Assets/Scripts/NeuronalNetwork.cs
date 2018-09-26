using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronalNetwork : MonoBehaviour {


    int[] layers;
    int[] Layers {get { return layers; } }
    float[][] neuronas;
    float[][][] pesos;
    float[][][] Pesos { get { return pesos; } }

    public NeuronalNetwork( int[] initilLayers)
    {
        layers = copyArray(initilLayers);
        InicializarNeuronas(layers);
        InicializarPesos(layers);

    }
    public NeuronalNetwork(NeuronalNetwork parent)
    {

        layers = parent.Layers;
        InicializarNeuronas(layers);
        CopiarPesos(parent.Pesos);
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
        float[] currentLayer = input;
        float neuronaActual;
        if(input.Length == layers[0])
        {
            for (int i = 1; i < layers.Length; i++)
            {
                currentLayer = new float[layers[i]];
                for (int j = 0; j < layers[i] -1; j++)
                {
                    for (int k = 0; k < pesos[i-1][j].Length; k++)
                    {
                        //neuronaActual *+´´¨*[Ñ]= (activacion) currentLayer peso[i][j][k]
                        
                    }
                    //Neurona[i][j] = neuronaActual
                    currentLayer[j] = neuronaActual;
                }                
            }
            return currentLayer;
        }
        else
        {
            Debug.LogError("Tamaño de input no coincide");
            return new float[0];
        }
        
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
                provitionalList.Add(0.0f);
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
            for (int j = 1; j < neuronalLayer[i]; j++)
            {
                provitionalList = new List<float>();
                for (int k = 0; k < neuronalLayer[i-1]; k++)
                {
                    value = UnityEngine.Random.Range(-0.5f, 0.5f);
                    provitionalList.Add(value);
                }
                provitionalArrayList.Add(provitionalList.ToArray());
            }
            pesos[i] = provitionalArrayList.ToArray();
        }

    }
    

}
