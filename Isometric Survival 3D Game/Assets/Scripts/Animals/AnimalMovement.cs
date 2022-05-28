using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    Animator animator;
    Queue<Vector3> newPositions = new Queue<Vector3>();
    PathFinding pathFinding;
    Map map;
    bool isMoving = false;

    private void Start()
    {
        pathFinding = FindObjectOfType<PathFinding>();
        map = FindObjectOfType<Map>();
        animator = GetComponent<Animator>();
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
            List<Node> nodes = FindPathFromAnimal(x, z);
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    newPositions.Enqueue(new Vector3(nodes[i].x, transform.position.y, nodes[i].z));
                }
                isMoving = true;
                return true;
            }
        }
        return false;
    }

    public List<Node> FindPathFromAnimal(int x, int z)
    {
        List<Node> nodes;
        if (map.GetNode(x, z).walkable == false)
        {
            map.GetNode(x, z).walkable = true;
            nodes = pathFinding.FindPath(GetX(), GetZ(), x, z);
            map.GetNode(x, z).walkable = false;
        }
        else
        {
            nodes = pathFinding.FindPath(GetX(), GetZ(), x, z);
        }
        return nodes;
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
                if (newPositions.Count > 0) Rotate(newPositions.Peek());
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            isMoving = false;
        }
    }

    private void Rotate(Vector3 newPosition)
    {
        Debug.Log("Rotate");
        Vector3 movementDirection = new Vector3((newPosition.x - transform.position.x), 0, (newPosition.z - transform.position.z));
        movementDirection.Normalize();
        transform.forward = movementDirection;
        animator.SetBool("isWalking", true);
    }

    public bool IsMoving()
    {
        return isMoving;
    }

}
