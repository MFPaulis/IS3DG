using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    int x, z;
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    float currentEnergyCost;
    Energy energy;
    Equipment equipment;
    GameObject block;
    bool readyToTake;
    [SerializeField] static float [] energyCost = { 50, 40, 30, 20 };
    [SerializeField] int addedParts = 1;
    [SerializeField] AudioClip audioClip;
    Tutorial tutorial;

    void Start()
    {
        tutorial = FindObjectOfType<Tutorial>();
        map = FindObjectOfType<Map>();
        block = transform.parent.gameObject;
        characterManager = FindObjectOfType<CharacterManager>();
        Node node = block.GetComponent<Node>();
        x = node.x;
        z = node.z;
    }

    private void Update()
    {
        if (readyToTake && !characterMovement.IsMoving())
        {
            AudioSource.PlayClipAtPoint(audioClip, characterManager.GetCamera().transform.position, 1);
            energy.DecreaseEnergy(currentEnergyCost);
            equipment.AddParts(addedParts);
            tutorial.TutorialAction(1);
            Destroy(gameObject);
        }
    }

    public void Clicked()
    {
        if(!readyToTake)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            equipment = characterManager.GetEquipment();
            currentEnergyCost = energyCost[characterManager.GetTechnicalSkill()];
            if (!characterMovement.IsMoving())
            {
                transform.parent.GetComponent<Node>().walkable = true;
                List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
                if (nodes != null && energy.GetEnergy()
                   >= characterMovement.getEnergyCost() * (nodes.Count - 1) + currentEnergyCost
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

    public static float GetEnergyCost(int skillLevel)
    {
        return energyCost[skillLevel];
    }
}
