using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    int x, z;
    Map map;
    CharacterManager characterManager;
    Energy energy;
    CharacterMovement characterMovement;
    Equipment equipment;
    CampEquipment campEquipment;
    public static GameObject activeTent;

    // Start is called before the first frame update
    void Start()
    {
        campEquipment = CampEquipment.campEquipment;
        equipment = GetComponent<Equipment>();
        equipment.setToCampEquipment();
        map = FindObjectOfType<Map>();
        characterManager = FindObjectOfType<CharacterManager>();
        Node node = gameObject.GetComponent<Node>();
        x = node.x;
        z = node.z;
    }

    public void showEquipment()
    {
        campEquipment.gameObject.SetActive(true);
        equipment.updateTexts();
    }


    public void Clicked()
    {
        characterMovement = characterManager.GetCharacterMovement();
        energy = characterManager.GetEnergy();
        if (!characterMovement.IsMoving())
        {
            List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
            if (nodes != null && energy.GetEnergy()
              >= characterMovement.getEnergyCost() * (nodes.Count - 1))
            {
                characterMovement.MoveToPoint(x, z);
            }
        }
    }
}
