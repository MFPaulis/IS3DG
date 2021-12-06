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
        
    }

    private void OnMouseUpAsButton()
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
