using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathDrawing : MonoBehaviour
{
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    CampEquipment campEquipment;
    Energy energy;
    List<Node> nodes = new List<Node>();
    bool pathShowed;
    int xGoal, zGoal;
    Costs costs;
    int energyCosts;
    int layerMask;
    bool spaceshipDrawn = false;

    void Start()
    {
        campEquipment = CampEquipment.campEquipment;
        characterManager = FindObjectOfType<CharacterManager>();
        map = FindObjectOfType<Map>();
        characterMovement = FindObjectOfType<CharacterMovement>();
        energy = FindObjectOfType<Energy>();
        costs = FindObjectOfType<Costs>();
        layerMask = LayerMask.GetMask("Blocks");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            costs.Clear();
            campEquipment.gameObject.SetActive(false);
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Block>())
                {
                    int x = hitObject.GetComponent<Node>().x;
                    int z = hitObject.GetComponent<Node>().z;
                    /*
                    if (hitObject.GetComponent<Block>().GetBType() == BlockType.Secret)
                    {
                        hitObject.GetComponent<Block>().BlockClicked();
                    }
                    else
                    */
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
                        if(hitObject.GetComponent<Block>().GetBType() == BlockType.Tent)
                        {
                            Tent.activeTent = hitObject;
                            hitObject.GetComponent<Tent>().showEquipment();
                        }
                    }
                }
                else if (pathShowed)
                {
                    ErasePath();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<EmptyPlace>())
                {
                    hitObject.GetComponent<EmptyPlace>().ClickedRight();
                }
            }

        }
    }

    private void DrawPath(int x, int z)
    {

        nodes = characterMovement.FindPathFromCharacter(x, z);
        if (nodes != null)//&& energy.GetEnergy() >= characterMovement.getEnergyCost() * (nodes.Count - 1))
        {
            foreach (Node node in nodes)
            {
                GameObject block = map.GetBlock(node.x, node.z);
                Material[] materials = block.GetComponent<Renderer>().materials;

                if (block.GetComponent<Block>().GetBType() == BlockType.Secret)
                {
                    materials[0].color = new Color(0.6f, 1f, 0.6f, 1);
                }
                materials[1].color = new Color(0.6f, 1f, 0.6f, 1);

            }
            if (map.GetBlock(x,z).GetComponent<Block>().GetBType() == BlockType.Spaceship)
            {
                DrawSpaceship();
                spaceshipDrawn = true;
            }
            pathShowed = true;
        }
    }

    public void ErasePath()
    {
        if (nodes != null)
        {
            foreach (Node node in nodes)
            {
                GameObject block = map.GetBlock(node.x, node.z);
                Material[] materials = block.GetComponent<Renderer>().materials;
                if (block.GetComponent<Block>().GetBType() == BlockType.Secret)
                {
                    materials[1].color = new Color32(154, 154, 154, 150);
                    materials[0].color = new Color32(154, 154, 154, 150);
                }
                else
                {
                    materials[1].color = new Color(0.87f, 0.97f, 1, 1);
                }

            }
            nodes.Clear();
            if (spaceshipDrawn)
            {
                EraseSpaceship();
            }
        }
        costs.resetCosts();
        pathShowed = false;
    }

    public void DrawSpaceship()
    {
        List <GameObject> spaceshipBlocks = map.GetSpaceshipBlocks();
        foreach (GameObject block in spaceshipBlocks)
        {
            Material[] materials = block.GetComponent<Renderer>().materials;
            materials[1].color = new Color(0.6f, 1f, 0.6f, 1);
        }
    }

    public void EraseSpaceship()
    {
        List<GameObject> spaceshipBlocks = map.GetSpaceshipBlocks();
        foreach (GameObject block in spaceshipBlocks)
        {
            Material[] materials = block.GetComponent<Renderer>().materials;
            materials[1].color = new Color(0.87f, 0.97f, 1, 1);
        }
        spaceshipDrawn = false;
    }
}
