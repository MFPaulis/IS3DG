using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmptyPlace : MonoBehaviour
{
    int x, z;
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    Energy energy;
    Equipment equipment;
    bool readyToBuild;
    [SerializeField] float energyCost = 20;
    [SerializeField] int woodCost = 5;
    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<Map>();
        characterManager = FindObjectOfType<CharacterManager>();
        Node node = gameObject.GetComponent<Node>();
        x = node.x;
        z = node.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToBuild && !characterMovement.IsMoving())
        {
            energy.DecreaseEnergy(energyCost);
            equipment.RemoveWood(woodCost);

            if (z != 0 && map.GetNode(x, z - 1).walkable)
            {
                map.SetCamp(x, z, 0, -1);
            }
            else if (x != map.GetWidth() - 1 && map.GetNode(x + 1, z).walkable)
            {
                map.SetCamp(x, z, 1, 0);
            }
            else if (x != 0 && map.GetNode(x - 1, z).walkable)
            {
                map.SetCamp(x , z, -1, 0);
            }
            else if (z != map.GetHeight() - 1 && map.GetNode(x, z + 1).walkable)
            {
                map.SetCamp(x, z, 0, 1);
            }
        }
    }
    /*
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!readyToBuild)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            equipment = characterManager.GetEquipment();
            if (eventData.button == PointerEventData.InputButton.Right && !characterMovement.IsMoving())
            {
                List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
                if (nodes != null && energy.GetEnergy()
                    >= characterMovement.getEnergyCost() * (nodes.Count - 1) + energyCost
                    && equipment.GetWood() >= woodCost && (characterMovement.MoveToPoint(x, z)))
                {
                    readyToBuild = true;
                }
            }
        }
    }*/

    public void ClickedRight()
    {
        if (!readyToBuild)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            equipment = characterManager.GetEquipment();
            if (!characterMovement.IsMoving())
            {
                List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
                if (nodes != null && energy.GetEnergy()
                    >= characterMovement.getEnergyCost() * (nodes.Count - 1) + energyCost
                    && equipment.GetWood() >= woodCost && (characterMovement.MoveToPoint(x, z)))
                {
                    readyToBuild = true;
                }
            }
        }
    }

    public void Clicked()
    {
        characterMovement = characterManager.GetCharacterMovement();
        characterMovement.MoveToPoint(x, z);
    }
}
