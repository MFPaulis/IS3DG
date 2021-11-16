using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBlock : MonoBehaviour
{
    int x, z;
    Map map;
    CharacterMovement characterMovement;
    Energy energy;
    List<GameObject> neighbours = new List<GameObject>();
    [SerializeField] float energyCost = 10;

    private void Start()
    {
        map = FindObjectOfType<Map>();
        characterMovement = FindObjectOfType<CharacterMovement>();
        energy = FindObjectOfType<Energy>();
        Node node = gameObject.GetComponent<Node>();
        x = node.x;
        z = node.z;
        checkIfAvailable();
    }

    private void AddNeighbours()
    {
        neighbours.Clear();
        if (x != 0)
            neighbours.Add(map.GetBlock(x - 1, z));
        if (x != map.GetWidth() - 1)
            neighbours.Add(map.GetBlock(x + 1, z));
        if (z != 0)
            neighbours.Add(map.GetBlock(x, z - 1));
        if (z != map.GetHeight() - 1)
            neighbours.Add(map.GetBlock(x, z + 1));
    }

    void checkIfAvailable()
    {
        AddNeighbours();
        gameObject.SetActive(false);
        foreach(GameObject neighbour in neighbours)
        {
            if (neighbour.GetComponent<SecretBlock>()) continue;
            gameObject.SetActive(true);
            break;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!characterMovement.IsMoving())
        {
            gameObject.GetComponent<Node>().walkable = true;
            List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
            gameObject.GetComponent<Node>().walkable = false;
            if (nodes != null && energy.GetEnergy()
                >= characterMovement.getEnergyCost() * (nodes.Count-2) + energyCost)
            {
                energy.DecreaseEnergy(energyCost);
                Node secondToLastNode = nodes[nodes.Count - 2];
                Node lastNode = nodes[nodes.Count - 1];
                map.Discover(x, z);
                if (!map.GetNode(lastNode.x, lastNode.z).walkable)
                {
                    characterMovement.MoveToPoint(secondToLastNode.x, secondToLastNode.z);
                } else
                {
                    characterMovement.MoveToPoint(lastNode.x, lastNode.z);
                }
                AddNeighbours();
                Destroy(gameObject);
                foreach (GameObject neighbour in neighbours)
                {
                    if (neighbour.GetComponent<SecretBlock>())
                        neighbour.GetComponent<SecretBlock>().checkIfAvailable();
                }
            }
        }
    }

    
}
