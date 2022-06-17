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
    float currentEnergyCost;
    bool readyToCook;
    [SerializeField] static float [] energyCost = { 40, 30, 20, 10 };
    [SerializeField] int removedFood = 1;
    [SerializeField] int addedCookedFood = 1;
    Tutorial tutorial;

    // Start is called before the first frame update
    void Start()
    {
        tutorial = FindObjectOfType<Tutorial>();
        characterManager = FindObjectOfType<CharacterManager>();
        Node node = gameObject.GetComponent<Node>();
        x = node.x;
        z = node.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToCook && !characterMovement.IsMoving())
        {
            GetComponent<AudioSource>().Play();
            energy.DecreaseEnergy(currentEnergyCost);
            equipment.RemoveFood(removedFood);
            equipment.AddCookedFood(addedCookedFood);
            readyToCook = false;
            tutorial.TutorialAction(11);
        }
    }


    public void Clicked()
    {
        characterMovement = characterManager.GetCharacterMovement();
        energy = characterManager.GetEnergy();
        equipment = characterManager.GetEquipment();
        currentEnergyCost = energyCost[characterManager.GetCookingSkill()];
        if (!characterMovement.IsMoving())
        {
            gameObject.GetComponent<Node>().walkable = true;
            List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
            gameObject.GetComponent<Node>().walkable = false;
            if (nodes != null && energy.GetEnergy()
                >= characterMovement.getEnergyCost() * (nodes.Count - 2) + currentEnergyCost
                && equipment.GetFood() >= removedFood)
            {
                Node secondToLastNode = nodes[nodes.Count - 2];
                Node lastNode = nodes[nodes.Count - 1];
                characterMovement.MoveToPoint(secondToLastNode.x, secondToLastNode.z);
                readyToCook = true;
            }
        }
    }

    public static float GetEnergyCost(int skillLevel)
    {
        return energyCost[skillLevel];
    }
}
