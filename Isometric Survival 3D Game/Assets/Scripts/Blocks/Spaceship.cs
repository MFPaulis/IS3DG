using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
   // [SerializeField] int x, z;
    [SerializeField] static float [] energyCost = { 50, 40, 30, 20 };
    [SerializeField] int removedParts = 1;
    [SerializeField] float requiredParts = 10;
    int repairProgress;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    Energy energy;
    Equipment equipment;
    bool readyToRepair;
    float currentEnergyCost;

    private void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
    }

    private void Update()
    {
        if (readyToRepair && !characterMovement.IsMoving())
        {
            GetComponent<AudioSource>().Play();
            energy.DecreaseEnergy(currentEnergyCost);
            equipment.RemoveParts(removedParts);
            repairProgress += removedParts;
            float percentage = repairProgress / requiredParts * 100;
            text.text = percentage + "%";
            readyToRepair = false;
            if (repairProgress >= requiredParts)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }

    public void Clicked(int x, int z)
    {
        characterMovement = characterManager.GetCharacterMovement();
        energy = characterManager.GetEnergy();
        equipment = characterManager.GetEquipment();
        currentEnergyCost = energyCost[characterManager.GetTechnicalSkill()];
        if (!characterMovement.IsMoving())
        {
            List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
            if (nodes != null && equipment.GetParts() >= removedParts && energy.GetEnergy()
               >= characterMovement.getEnergyCost() * (nodes.Count - 1) + currentEnergyCost
               && (characterMovement.MoveToPoint(x, z)))
            {
                readyToRepair = true;
            }
        }
    }

    public static float GetEnergyCost(int skillLevel)
    {
        return energyCost[skillLevel];
    }

}
