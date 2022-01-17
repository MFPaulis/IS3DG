using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrub : MonoBehaviour
{
    int x, z;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    float currentEnergyCost;
    Energy energy;
    Equipment equipment;
    GameObject block;
    Node node;
    GameObject berries;
    bool readyToGather;
    int countDown;
    [SerializeField] static float [] energyCost = { 30, 20, 10, 0 };
    [SerializeField] int addedFood = 1;
    [SerializeField] int daysToGrow = 6;
    // Start is called before the first frame update
    void Start()
    {
        berries = gameObject.transform.GetChild(0).gameObject;
        characterManager = FindObjectOfType<CharacterManager>();
        equipment = FindObjectOfType<Equipment>();
        block = transform.parent.gameObject;
        node = block.GetComponent<Node>();
        x = node.x;
        z = node.z;
        countDown = daysToGrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToGather && !characterMovement.IsMoving())
        {
            energy.DecreaseEnergy(currentEnergyCost);
            equipment.AddFood(addedFood);
            berries.SetActive(false);
            readyToGather = false;
        }
    }

    public void Clicked()
    {
        if(!readyToGather)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            currentEnergyCost = energyCost[characterManager.GetGatheringSkill()];
            if (!characterMovement.IsMoving() && !readyToGather && berries.activeSelf)
            {
                transform.parent.GetComponent<Node>().walkable = true;
                List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
                if (nodes != null && energy.GetEnergy()
                   >= characterMovement.getEnergyCost() * (nodes.Count - 1) + currentEnergyCost
                   && (characterMovement.MoveToPoint(x, z)))
                {
                    readyToGather = true;
                }
                else
                {
                    transform.parent.GetComponent<Node>().walkable = false;
                }
            } 
            if (characterMovement.GetX() == x && characterMovement.GetZ() == z)
            {
                node.walkable = true;
            } else
            {
                node.walkable = false;
            }
        }
    }

    void GrowBerries()
    {
        berries.SetActive(true);
    }

    public void CountDown()
    {
        if(!berries.activeSelf) countDown--;
        if(countDown == 0)
        {
            GrowBerries();
            countDown = daysToGrow;
        }
    }

    public float GetEnergyCost(int skillLevel)
    {
        if (berries.activeSelf) return energyCost[skillLevel];
        else return 0;
    }


}
