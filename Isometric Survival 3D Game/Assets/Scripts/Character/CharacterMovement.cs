using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    float energyCost = 10;
    Queue<Vector3> newPositions = new Queue<Vector3>();
    PathFinding pathFinding;
    Energy energy;
    Sight sight;
    bool isMoving = false;

    private void Start()
    {
        pathFinding = FindObjectOfType<PathFinding>();
        energy = FindObjectOfType<Energy>();
        sight = gameObject.GetComponent<Sight>();
    }
    
    public int GetX()
    {
        return (int)Math.Round(transform.position.x);
    }
    public int GetZ()
    {
        return (int)Math.Round(transform.position.z);
    }
    
    public bool MoveToPoint(int x, int z)
    {
        if (!isMoving)
        {
            if (x == GetX() && z == GetZ()) return true;
            List<Node> nodes = FindPathFromCharacter(x, z);
            if (nodes != null)
            {
                if (energyCost * (nodes.Count - 1) <= energy.GetEnergy())
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        newPositions.Enqueue(new Vector3(nodes[i].x, transform.position.y, nodes[i].z));
                        if (i != 0) energy.DecreaseEnergy(energyCost);
                    }
                    isMoving = true;
                    return true;
                }
            }
        }
        return false;
    }

    public List<Node> FindPathFromCharacter(int x, int z)
    {
        return pathFinding.FindPath(GetX(), GetZ(), x, z);
    }

    private void Update()
    {
        if (newPositions.Count != 0)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPositions.Peek(), step);
            if (Vector3.Distance(transform.position, newPositions.Peek()) < 0.001f)
            {
                newPositions.Dequeue();
                sight.LookAround();
            }
        } else
        {
            isMoving = false;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public float getEnergyCost()
    {
        return energyCost;
    }

}
