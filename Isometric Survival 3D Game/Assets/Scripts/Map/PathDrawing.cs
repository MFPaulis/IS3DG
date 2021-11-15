using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    Map map;
    CharacterMovement characterMovement;
    Energy energy;
    List<Node> nodes = new List<Node>();
    bool pathShowed;
    int xGoal, zGoal;

    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<Map>();
        characterMovement = FindObjectOfType<CharacterMovement>();
        energy = FindObjectOfType<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.GetComponent<EmptyPlace>())
            {
                int x = hitObject.GetComponent<Node>().x;
                int z = hitObject.GetComponent<Node>().z;
                if (pathShowed && xGoal == x && zGoal == z)
                {
                    characterMovement.MoveToPoint(x, z);
                    ErasePath();
                }
                else if (!characterMovement.IsMoving())
                {
                    ErasePath();
                    DrawPath(x, z);
                    xGoal = x;
                    zGoal = z;
                    
                }
            }
            else if (pathShowed)
            {
                ErasePath();
            }
        }
    }

    private void DrawPath(int x, int z)
    {
        nodes = characterMovement.FindPathFromCharacter(x, z);
        if (nodes != null && energy.GetEnergy() >= characterMovement.getEnergyCost() * (nodes.Count - 1))
        {
            foreach (Node node in nodes)
            {
                GameObject block = map.GetBlock(node.x, node.z);
                Material[] materials = block.GetComponent<Renderer>().materials;
                materials[1].color = new Color(0.6f, 1f, 0.6f, 1);
            }
            pathShowed = true;
        }
    }

    private void ErasePath()
    {
        foreach (Node node in nodes)
        {
            GameObject block = map.GetBlock(node.x, node.z);
            Material[] materials = block.GetComponent<Renderer>().materials;
            materials[1].color = new Color(0.87f, 0.97f, 1, 1);
        }
        nodes.Clear();
        pathShowed = false;
    }
}
