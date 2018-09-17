using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grilla : MonoBehaviour {

    [SerializeField] Vector2 dimensiones;
    int mask;
    Vector3 origin;
    Node[,] nodes;
    [SerializeField] float nodeDiameter;
    int nodesOnX;
    int nodesOnY;
    bool esCaminable;

    //public Transform player;


    void Awake()
    {
        mask = LayerMask.NameToLayer("UnWalkable");
        calculateDimensions();
        crearGrilla();        
    }    

    private void calculateDimensions()
    {
        nodesOnX = Mathf.FloorToInt(dimensiones.x / nodeDiameter);
        nodesOnY = Mathf.FloorToInt(dimensiones.y / nodeDiameter);
        nodes = new Node[nodesOnX, nodesOnY];
    }

    void crearGrilla()
    {
        origin = transform.position - Vector3.right * (dimensiones.x / 2) - Vector3.forward * (dimensiones.y / 2);
      
        for (int x = 0; x < nodesOnX; x++)
        {
            for (int y = 0; y < nodesOnY; y++)
            {
                Vector3 nodePos = origin + Vector3.right * (x * nodeDiameter + nodeDiameter/2) + Vector3.forward* ( y * nodeDiameter + nodeDiameter/2);                
               
                esCaminable = !Physics.CheckSphere(nodePos,nodeDiameter/2,1<<mask);
                nodes[x, y] = new Node(nodePos, new Vector2(x,y),esCaminable);
            }
        }


    }

    public List<Node> GetNeighborsNodes(Node myNode)
    {
        List<Node> neighbords = new List<Node>();
        Vector2 coord = myNode.Pos;
        int count = 0;
        for (int i = (int)coord.x-1; i <= coord.x+1; i++)
        {
            for (int j = (int)coord.y - 1; j <= coord.y + 1; j++)
            {
                if(i != coord.x || j != coord.y)
                {
                    if (i >= 0 && j >= 0 && i <= nodesOnX - 1 && j <= nodesOnY - 1)
                    {
                        neighbords.Add(nodes[i, j]);
                        count++;
                    }

                }                    
            }
        }
        return neighbords;

    }

    public Node getNodeFromWorldPosition(Vector3 pos)
    {
        int x;
        int y;
        Vector3 localPos = pos - origin;
        x = Mathf.FloorToInt(localPos.x / nodeDiameter);
        y = Mathf.FloorToInt(localPos.z / nodeDiameter);
            
         
        if (x >= 0 && x < nodesOnX && y >= 0 && y < nodesOnY)
            return nodes[x, y];
        else
            throw new EntryPointNotFoundException();

       
    }

    void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(dimensiones.x, 1 , dimensiones.y));
   

        if (nodes != null)
        {
            //Node playerNode = getNodeFromWorldPosition(player.position);
            foreach (Node n in nodes)
            {
                /*if(playerNode == n)                
                    Gizmos.color = Color.blue;          

                
                else */
                Gizmos.color = (n.EsCaminable) ? Color.green : Color.red;

                if (n.recorrido)
                    Gizmos.color = Color.cyan;

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.05f));
                
            }
            
           
        }


    }

   
}

public class Node
{
    public int h;
    public int g;    
    public int f; 
    bool esCaminable;
    public bool recorrido = false;
    public bool EsCaminable { get { return esCaminable; } }
    public Vector3 worldPosition;
    Vector2 pos;
    public Vector2 Pos { get { return pos; } }
    Node parent;
    public Node Parent { get { return parent; } }
    public Node(Vector3 worldPos, Vector2 coord, bool esCaminable)
    {
        worldPosition = worldPos;
        pos = coord;
        this.esCaminable = esCaminable;
        
    }

    

    public void calculateF(Node from, Vector2 goal)
    {
        parent = from;
        h = CalculateDistance(pos, goal);
        g = CalculateDistance(pos, from.pos);
        g += from.g;
        f = h + g;       
    }

    

    int CalculateDistance(Vector2 from, Vector2 to)
    {
        int distance = 0;
        int x = (int)Mathf.Abs(to.x - from.x);
        int y = (int)Mathf.Abs(to.y - from.y);
        if (x <= y)
            distance += x * 14;
        else
            distance += y * 14;

        distance += Mathf.Abs(x - y) * 10;
        return distance;

    }


}
