using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woods : MonoBehaviour
{
    int x, z;
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    Energy energy;
    Equipment equipment;
    GameObject block;
    bool readyToCutDown;
    bool isFalling = false;
    [SerializeField] static float energyCost = 20;
    [SerializeField] int fallingSpeed = 5;
    [SerializeField] int addedWood = 1;

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

    // Update is called once per frame
    void Update()
    {
        if(isFalling)
        {
            transform.Rotate(new Vector3(30, 0, 0) * (fallingSpeed * Time.deltaTime));
        }
        else if(readyToCutDown && !characterMovement.IsMoving())
        {
            energy.DecreaseEnergy(energyCost);
            equipment.AddWood(addedWood);
            transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            isFalling = true;
            Destroy(gameObject, 0.8f);
        }
    }



    public void Clicked()
    {
        if(!readyToCutDown)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            if (!characterMovement.IsMoving() && !readyToCutDown)
            {
                transform.parent.GetComponent<Node>().walkable = true;
                List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
                if (nodes != null && energy.GetEnergy()
                   >= characterMovement.getEnergyCost() * (nodes.Count - 1) + energyCost
                   && (characterMovement.MoveToPoint(x, z)))
                {
                    transform.SetParent(null, true);
                    map.Clean(x, z);
                    readyToCutDown = true;
                } else
                {
                    transform.parent.GetComponent<Node>().walkable = false;
                }
            }
        }
        
    }

    public static float GetEnergyCost()
    {
        return energyCost;
    }
}