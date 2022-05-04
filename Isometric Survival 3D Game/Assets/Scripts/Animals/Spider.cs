using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Animal
{
    int x, z;
    Map map;
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    AnimalManager animalManager;
    AnimalMovement animalMovement;
    Energy energy;
    Equipment equipment;
    Life life;
    GameObject block;
    float currentEnergyCost;
    [SerializeField] static float[] energyCost = { 30, 20, 10, 0 };
    [SerializeField] int addedFood = 3;
    [SerializeField] int decreasedLife = 30;
    bool readyToAttack;

    private void Start()
    {
        animalManager = FindObjectOfType<AnimalManager>();
        animalMovement = GetComponent<AnimalMovement>();
        map = FindObjectOfType<Map>();
        block = transform.parent.gameObject;
        characterManager = FindObjectOfType<CharacterManager>();
        Node node = block.GetComponent<Node>();
        x = node.x;
        z = node.z;
    }

    void Update()
    {
        if (readyToAttack && !characterMovement.IsMoving())
        {
            int rnd = Random.Range(0, 2);
            GetComponent<AudioSource>().Play();
            energy.DecreaseEnergy(currentEnergyCost);
            if (rnd == 0)
            {
                life.DecreaseLife(decreasedLife);
                readyToAttack = false;
            } else
            {
                equipment.AddFood(addedFood);
                transform.SetParent(null, true);
                map.Clean(x, z);
                animalManager.RemoveAnimal(this);
                Destroy(gameObject);
            }
        }
    }

    public void Clicked()
    {
        characterMovement = characterManager.GetCharacterMovement();
        energy = characterManager.GetEnergy();
        equipment = characterManager.GetEquipment();
        life = characterManager.GetLife();
        currentEnergyCost = GetEnergyCost(characterManager.GetGatheringSkill());
        if (!characterMovement.IsMoving() && !readyToAttack)
        {
            transform.parent.GetComponent<Node>().walkable = true;
            List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
            if (nodes != null && energy.GetEnergy()
                >= characterMovement.getEnergyCost() * (nodes.Count - 1) + currentEnergyCost
                && characterMovement.MoveToPoint(x, z))
            {
                readyToAttack = true;
            }
            else
            {
                transform.parent.GetComponent<Node>().walkable = false;
            }
        }
    }

    public static float GetEnergyCost(int skillLevel)
    {
        return energyCost[skillLevel];
    }

    public override void Move()
    {
        bool isSomethingAround = false;
        List<Block> blocks = new List<Block>();
        for(int i = -3; i < 4; i++)
        {
            int j = -Mathf.Abs(i) + 3;
            for (int k = -j; k < j + 1; k++)
            {
                Block block = map.GetBlock(x+i, z+k).GetComponent<Block>();
                
                if (block != null)
                {
                    if (animalMovement.FindPathFromAnimal(x + i, z + k) != null)
                    {
                        BlockType bType = block.GetBType();
                        if(bType == BlockType.Empty)// || bType == BlockType.Parts || bType == BlockType.Shrub || bType == BlockType.Tent)
                        {
                            blocks.Add(block);
                            if (bType != BlockType.Empty)
                            {
                                isSomethingAround = true;
                            }
                        }
                    }
                }
            }
        }

        if(isSomethingAround)
        {
            foreach (Block block in blocks)
            {
                if (block.GetBType() == BlockType.Empty)
                {
                    blocks.Remove(block);
                }
            }
        }
        if (blocks.Count > 0)
        {
            int rnd = Random.Range(0, blocks.Count);
            Block chosenBlock = blocks[rnd];
            Node node = chosenBlock.gameObject.GetComponent<Node>();
            if (animalMovement.MoveToPoint(node.x, node.z))
            {
                transform.SetParent(map.GetBlock(node.x, node.z).transform, true);
                map.Clean(x, z);
                x = node.x;
                z = node.z;
                transform.parent.GetComponent<Block>().SetBType(BlockType.Spider);
            }
        }
    }
}
