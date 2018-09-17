using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrella : MonoBehaviour {
    
    [SerializeField]grilla myGrill;
    Vector3[] path;
    float t;
	
	public Vector3[] getPath(Transform start, Transform goal)
    {
        if (FindPath(goal, start))
        {            
              return path;            
        }
        else
            return new Vector3[0];

    }

    bool FindPath(Transform objetive, Transform start)
    {
        Node startingNode = myGrill.getNodeFromWorldPosition(start.position);
        Node objetiveNode = myGrill.getNodeFromWorldPosition(objetive.position);
        List<Node> nodosHabilitados = new List<Node>();
        List<Node> nodosCerrados = new List<Node>();
        nodosHabilitados.Add(startingNode);
        Node currentNode = startingNode;
        currentNode.calculateF(startingNode, objetiveNode.Pos);
        List<Node> currentNeighbords;
        Debug.Log("Debo llegar a" + objetiveNode.Pos);
        
        while (nodosHabilitados.Count > 0)
        {
            currentNode = nodosHabilitados[0];
            for (int i = 1; i < nodosHabilitados.Count; i++)
            {
                if(nodosHabilitados[i].f<= currentNode.f)
                {
                    if (nodosHabilitados[i].h < currentNode.h)
                    {
                        currentNode = nodosHabilitados[i];
                    }                    
                }               
            }
            
            nodosHabilitados.Remove(currentNode);
            nodosCerrados.Add(currentNode);
            //Debug.Log("me quedan " + nodosHabilitados.Count + " Nodos");

            if (currentNode == objetiveNode)
            {
                Debug.Log("Llegue a " + objetiveNode.Pos);
                    while(currentNode != startingNode)
                    {
                    CalculatePath(objetiveNode, startingNode);
                        return true;
                    }
                
            }

            currentNeighbords = myGrill.GetNeighborsNodes(currentNode);

            for (int i = 0; i < currentNeighbords.Count; i++)
            {
                if (currentNeighbords[i].EsCaminable)
                {
                    if (nodosHabilitados.Contains(currentNeighbords[i]))
                    {
                        //Debug.Log("este ia existe y recalculo");                       
                        Node lastNode = currentNeighbords[i].Parent;
                        currentNeighbords[i].calculateF(currentNode, objetiveNode.Pos);
                        if (currentNeighbords[i].f > currentNode.f)
                        {
                            //Debug.Log("pos era mas pesado, lo vuelvo pa tras");
                            currentNeighbords[i].calculateF(lastNode, objetiveNode.Pos);                        
                        }
                    }
                    else if (!nodosCerrados.Contains(currentNeighbords[i]))
                    {
                        
                        currentNeighbords[i].calculateF(currentNode, objetiveNode.Pos);
                        //Debug.Log("agrego el nodo nuevo con f = " + currentNeighbords[i].f);
                        nodosHabilitados.Add(currentNeighbords[i]);
                    }
                }
                
            }
           
        }



        return false;


    }

    private void CalculatePath(Node objetiveNode, Node startingNode)
    {
        List<Node> provitionalList = new List<Node>();
        Node currentNode = objetiveNode;

        while(currentNode != startingNode)
        {
            provitionalList.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path = new Vector3[provitionalList.Count];
        for (int i = 0; i < provitionalList.Count; i++)
        {
            path[path.Length - (i + 1)] = provitionalList[i].worldPosition;
        }
    }
     

}


