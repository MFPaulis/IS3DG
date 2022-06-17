using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBlock : MonoBehaviour
{
    int x, z;
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    PathDrawing pathDrawing;
    Energy energy;
    List<GameObject> neighbours = new List<GameObject>();
    [SerializeField] float energyCost = 10;
    BlockType type = BlockType.Secret;
    public bool blocked = false;
    Material[] materials;
    int activationArea = -1;
    Tutorial tutorial;

    private void Start()
    {
        tutorial = FindObjectOfType<Tutorial>();
        materials = GetComponent<MeshRenderer>().materials;
        pathDrawing = FindObjectOfType<PathDrawing>();
        map = FindObjectOfType<Map>();
        characterManager = FindObjectOfType<CharacterManager>();
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

    public void ActionsOfPassages()
    {
        if (activationArea != -1)
        {
            map.ShowArea(activationArea);
        }
        tutorial.TutorialAction(0);
        if (activationArea == 0) tutorial.TutorialAction(3);
        if (activationArea == 1) tutorial.TutorialAction(6);
        if (activationArea == 2) tutorial.TutorialAction(13);
        if (activationArea == 3) tutorial.TutorialAction(16);
    }

    public void Clicked()
    {
        if(!blocked)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            if (!characterMovement.IsMoving())
            {
                gameObject.GetComponent<Node>().walkable = true;
                List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
                gameObject.GetComponent<Node>().walkable = false;
                if (nodes != null && energy.GetEnergy()
                    >= characterMovement.getEnergyCost() * (nodes.Count - 2) + energyCost)
                {
                    pathDrawing.ErasePath();
                    energy.DecreaseEnergy(energyCost);
                    Node secondToLastNode = nodes[nodes.Count - 2];
                    Node lastNode = nodes[nodes.Count - 1];
                    map.Discover(x, z);
                    if (!map.GetNode(lastNode.x, lastNode.z).walkable)
                    {
                        characterMovement.MoveToPoint(secondToLastNode.x, secondToLastNode.z);
                    }
                    else
                    {
                        characterMovement.MoveToPoint(lastNode.x, lastNode.z);
                    }
                }
            }
        }
    }

    public void CheckNeighbours()
    {
        AddNeighbours();
        foreach (GameObject neighbour in neighbours)
        {
            if (neighbour.GetComponent<SecretBlock>())
                neighbour.GetComponent<SecretBlock>().checkIfAvailable();
        }
    }
    
    public BlockType GetBlockType()
    {
        return type;
    }

    public void SetType(BlockType type)
    {
        this.type = type;
    }

    public void SetActivationArea(int n)
    {
        activationArea = n;
    }

    public int GetActivationArea()
    {
        return activationArea;
    }

    public void SetBlocked(bool blocked)
    {
        Debug.Log("SetBlocked x " + x + " z " + z);
        float xx = gameObject.transform.position.x;
        float zz = gameObject.transform.position.z;
        this.blocked = blocked;
        if (blocked == true)
        {
            gameObject.transform.position = new Vector3(xx, -0.5f, zz);

            //materials[0].color = new Color(1f, 0, 0, 0.75f);
        } else
        {
            gameObject.transform.position = new Vector3(xx, 0, zz);
            //materials[0].color = new Color(0, 1, 0, 0.5f);
        }
    }
}
