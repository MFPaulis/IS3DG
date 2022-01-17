using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    Energy energy;
    List<Node> nodes = new List<Node>();
    bool pathShowed;
    int xGoal, zGoal;
    Costs costs;
    int energyCosts;
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        map = FindObjectOfType<Map>();
        characterMovement = FindObjectOfType<CharacterMovement>();
        energy = FindObjectOfType<Energy>();
        costs = FindObjectOfType<Costs>();
        layerMask = LayerMask.GetMask("Blocks");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            costs.Clear();
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            /*RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            GameObject hitObject = null;
            foreach (RaycastHit hit in hits)
            {
                hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Block>()) break;
            }*/
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Block>())
                {
                    int x = hitObject.GetComponent<Node>().x;
                    int z = hitObject.GetComponent<Node>().z;
                    if (pathShowed && xGoal == x && zGoal == z)
                    {
                        hitObject.GetComponent<Block>().BlockClicked();
                        ErasePath();
                    }
                    else if (!characterMovement.IsMoving())
                    {
                        ErasePath();
                        DrawPath(x, z);
                        xGoal = x;
                        zGoal = z;
                        costs.ShowCosts(x, z, hitObject.GetComponent<Block>());
                    }
                }
                else if (pathShowed)
                {
                    ErasePath();
                }
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
        if (nodes != null)
        {
            foreach (Node node in nodes)
            {
                GameObject block = map.GetBlock(node.x, node.z);
                Material[] materials = block.GetComponent<Renderer>().materials;
                materials[1].color = new Color(0.87f, 0.97f, 1, 1);
            }
            nodes.Clear();
        }
        pathShowed = false;
    }
}
