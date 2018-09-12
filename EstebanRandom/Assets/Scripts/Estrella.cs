using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrella : MonoBehaviour {

    
   
    [SerializeField] Transform objetivePos;
    [SerializeField] Transform startingPos;
    [SerializeField]grilla myGrill;
    float t;
	// Use this for initialization

	void Start () {

        FindPath(objetivePos, startingPos);
    }
	
	// Update is called once per frame
	void Update () {
        /*if (t > 1)
        {
            FindPath(objetivePos, startingPos);
            t = 0;
        }
        t += Time.deltaTime;*/
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
        int qseio = 0;
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

            currentNode.recorrido = true;
            
            nodosHabilitados.Remove(currentNode);
            nodosCerrados.Add(currentNode);
            //Debug.Log("me quedan " + nodosHabilitados.Count + " Nodos");

            if (currentNode == objetiveNode)
            {
                Debug.Log("Llegue a " + objetiveNode.Pos);
                return true;
            }

            currentNeighbords = myGrill.GetNeighborsNodes(currentNode);

            for (int i = 0; i < currentNeighbords.Count; i++)
            {
                if (currentNeighbords[i].EsCaminable)
                {
                    if (nodosHabilitados.Contains(currentNeighbords[i]))
                    {
                        //Debug.Log("este ia existe y recalculo");
                        int neighbordG = currentNeighbords[i].g;
                        int neighbordH = currentNeighbords[i].h;
                        int neighbordF = currentNeighbords[i].f;
                        currentNeighbords[i].calculateF(currentNode, objetiveNode.Pos);
                        if (currentNeighbords[i].f > currentNode.f)
                        {
                            //Debug.Log("pos era mas pesado, lo vuelvo pa tras");
                            currentNeighbords[i].g = neighbordG;
                            currentNeighbords[i].h = neighbordH;
                            currentNeighbords[i].f = neighbordF;
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
            if (qseio > 100){
                Debug.Log("Force quit");
                return false;
            }
            qseio++;
        }
        return false;


    }
    /*void loop()
    {
        nodosHabilitados = calculateNode(currentNodo);
    }

    void calculateNextNode(Node currentNode)
    {
        if(nodosCerrados.Contains(node))

        nodosCerrados.Add(nodo);
    }*/

}


