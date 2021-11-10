using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spaceship : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] int x, z;
    [SerializeField] int energyCost = 20;
    [SerializeField] int removedParts = 1;
    [SerializeField] float requiredParts = 10;
    int repairProgress;
    CharacterMovement characterMovement;
    Energy energy;
    Equipment equipment;
    bool readyToRepair;

    private void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        energy = FindObjectOfType<Energy>();
        equipment = FindObjectOfType<Equipment>();
    }

    private void Update()
    {
        if (readyToRepair && !characterMovement.IsMoving())
        {
            energy.DecreaseEnergy(energyCost);
            equipment.RemoveParts(removedParts);
            repairProgress += removedParts;
            float percentage = repairProgress / requiredParts * 100;
            text.text = percentage + "%";
            readyToRepair = false;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!characterMovement.IsMoving())
        {
            List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
            if (nodes != null && equipment.GetParts() >= removedParts && energy.GetEnergy()
               >= characterMovement.getEnergyCost() * (nodes.Count - 1) + energyCost
               && (characterMovement.MoveToPoint(x, z)))
            {
                readyToRepair = true;
            }
        }
    }
}
