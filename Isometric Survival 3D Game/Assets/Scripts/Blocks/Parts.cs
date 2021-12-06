using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    int x, z;
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    Energy energy;
    Equipment equipment;
    GameObject block;
    bool readyToTake;
    [SerializeField] float energyCost = 10;
    [SerializeField] int addedParts = 1;


    void Start()
    {
        map = FindObjectOfType<Map>();
        block = transform.parent.gameObject;
        characterManager = FindObjectOfType<CharacterManager>();
        equipment = FindObjectOfType<Equipment>();
        Node node = block.GetComponent<Node>();
        x = node.x;
        z = node.z;
    }

    private void Update()
    {
        if (readyToTake && !characterMovement.IsMoving())
        {
            energy.DecreaseEnergy(energyCost);
            equipment.AddParts(addedParts);
            Destroy(gameObject);
        }
    }

    private void OnMouseUpAsButton()
    {
        if(!readyToTake)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            if (!characterMovement.IsMoving())
            {
                transform.parent.GetComponent<Node>().walkable = true;
                List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
                if (nodes != null && energy.GetEnergy()
                   >= characterMovement.getEnergyCost() * (nodes.Count - 1) + energyCost
                   && (characterMovement.MoveToPoint(x, z)))
                {
                    transform.SetParent(null, true);
                    map.Clean(x, z);
                    readyToTake = true;
                }
                else
                {
                    transform.parent.GetComponent<Node>().walkable = false;
                }
            }
        }
       
    }
}
