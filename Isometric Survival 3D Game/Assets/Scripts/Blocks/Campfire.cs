using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    int x, z;
    CharacterManager characterManager;
    Energy energy;
    CharacterMovement characterMovement;
    Equipment equipment;
    bool readyToCook;
    [SerializeField] float energyCost = 20;
    [SerializeField] int removedFood = 1;
    [SerializeField] int addedCookedFood = 1;

    // Start is called before the first frame update
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        equipment = FindObjectOfType<Equipment>();
        Node node = gameObject.GetComponent<Node>();
        x = node.x;
        z = node.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToCook && !characterMovement.IsMoving())
        {
            energy.DecreaseEnergy(energyCost);
            equipment.RemoveFood(removedFood);
            equipment.AddCookedFood(addedCookedFood);
            readyToCook = false;
        }
    }


    private void OnMouseUpAsButton()
    {
        characterMovement = characterManager.GetCharacterMovement();
        energy = characterManager.GetEnergy();
        if (!characterMovement.IsMoving())
        {
            gameObject.GetComponent<Node>().walkable = true;
            List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
            gameObject.GetComponent<Node>().walkable = false;
            if (nodes != null && energy.GetEnergy()
                >= characterMovement.getEnergyCost() * (nodes.Count - 2) + energyCost
                && equipment.GetFood() >= removedFood)
            {
                Node secondToLastNode = nodes[nodes.Count - 2];
                Node lastNode = nodes[nodes.Count - 1];
                characterMovement.MoveToPoint(secondToLastNode.x, secondToLastNode.z);
                readyToCook = true;
            }
        }
    }
}
