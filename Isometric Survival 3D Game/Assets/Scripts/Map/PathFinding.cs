using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour
{
    private Map map;
    private List<Node> open = new List<Node>();
    private List<Node> closed = new List<Node>();

    Node startNode;
    Node goalNode;

    private void Start()
    {
        map = FindObjectOfType<Map>();
    }

    public List<Node> FindPath(int startX, int startZ, int endX, int endZ)
    {
        startNode = map.GetNode(startX, startZ);
        goalNode = map.GetNode(endX, endZ);
        open.Clear();
        closed.Clear();
        startNode.cameFromNode = null;
        open.Add(startNode);
        while (open.Count() != 0)
        {
            //wybierz z open node o najmniejszym f i zapisz do thisNode.
            open = open.OrderBy(p => p.F).ToList<Node>();
            Node thisNode = open.ElementAt(0);
            closed.Add(thisNode); //przenieœ thisNode do openset
            open.RemoveAt(0);
            //jeœli osi¹gniêto cel zwróæ œcie¿kê
            if (thisNode.Equals(goalNode))
            {
                return CalculatePath();
            }
            //dla ka¿dego s¹siada..
            List<Node> neighbours = getNeighbours(thisNode);
            foreach (Node neighbour in neighbours)
            {
                //jeœli nie jest walkable lub jest w closed, kontynuuj
                if (!neighbour.walkable) continue;
                if (closed.Contains(neighbour)) continue;

                //zapisz próbne G
                int tentativeG = getNeighbourDistance(thisNode, neighbour) + thisNode.G;
                bool tentativeIsBetter = false;
                if (!open.Contains(neighbour))
                {
                    open.Add(neighbour);
                    neighbour.H = (int)(Vector2.Distance(neighbour.toVector(), goalNode.toVector()) * 10);
                    tentativeIsBetter = true;
                } else if (tentativeG < neighbour.G)
                {
                    tentativeIsBetter = true;
                }
                if(tentativeIsBetter)
                {
                    neighbour.cameFromNode = thisNode;
                    neighbour.G = tentativeG;
                    neighbour.F = neighbour.G + neighbour.H;
                }
            }
        }
        return null;
    }

    
    private int getNeighbourDistance(Node node1, Node node2)
    {
        if (node1.x == node2.x || node1.z == node2.z) return 10;
        else return 14;
    }

    private List<Node> getNeighbours(Node thisNode)
    {
        List<Node> neighbours = new List<Node>();
        if (thisNode.x != 0)
            neighbours.Add(map.GetNode(thisNode.x - 1, thisNode.z));
        if (thisNode.x != map.GetWidth() - 1) 
            neighbours.Add(map.GetNode(thisNode.x + 1, thisNode.z));
        if (thisNode.z != 0) 
            neighbours.Add(map.GetNode(thisNode.x, thisNode.z - 1));
        if (thisNode.z != map.GetHeight() - 1) 
            neighbours.Add(map.GetNode(thisNode.x, thisNode.z + 1));
        if (thisNode.x != 0 && thisNode.z != 0)
            neighbours.Add(map.GetNode(thisNode.x - 1, thisNode.z - 1));
        if (thisNode.x != map.GetWidth() - 1 && thisNode.z != 0)
            neighbours.Add(map.GetNode(thisNode.x + 1, thisNode.z - 1));
        if (thisNode.x != 0 && thisNode.z != map.GetHeight() - 1)
            neighbours.Add(map.GetNode(thisNode.x - 1, thisNode.z + 1));
        if (thisNode.x != map.GetWidth() - 1 && thisNode.z != map.GetHeight() - 1)
            neighbours.Add(map.GetNode(thisNode.x + 1, thisNode.z + 1));
        return neighbours;
    }

    private List<Node> CalculatePath()
    {
        List<Node> path = new List<Node>();
        path.Add(goalNode);
        Node currentNode = goalNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
}
