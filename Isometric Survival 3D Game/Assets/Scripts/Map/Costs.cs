using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Costs : MonoBehaviour
{
    CharacterManager characterManager;
    CharacterMovement characterMovement;
    Energy characterEnergy;
    Map map;
    [SerializeField] TextMeshProUGUI text;

    public void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        map = FindObjectOfType<Map>();
        characterEnergy = characterManager.GetEnergy();
    }

    public void Clear()
    {
        text.text = "";
    }

    public void ShowCosts(int x, int z, Block block)
    {
        characterEnergy = characterManager.GetEnergy();

        float costOfMoving = GetCostOfMoving(x, z);
        float costOfAction = GetCostOfAction(block);
        string actionString;

        if (block.GetBType() == BlockType.Campfire)
        {
            costOfMoving -= characterMovement.getEnergyCost();
        }

        if (costOfAction != 0)
        {
            actionString = "+" + costOfAction.ToString();
        } else
        {
            actionString = "";
        }
        float t = characterEnergy.GetEnergy() - (costOfMoving + costOfAction);

        Debug.Log(t);

        characterEnergy.setTempEnergy(t);

        text.text = "energy cost: " + costOfMoving.ToString() + actionString;
    }

    public float GetCostOfMoving(int x, int z)
    {
        characterMovement = characterManager.GetCharacterMovement();
        List<Node> nodes = characterMovement.FindPathFromCharacter(x, z);
        if (nodes == null) return 0;
        float costs = characterMovement.getEnergyCost() * (nodes.Count - 1);
        return costs;
    }

    public float GetCostOfAction(Block block)
    {
        characterMovement = characterManager.GetCharacterMovement();
        if (block.GetBType() == BlockType.Woods)
        {
            return Woods.GetEnergyCost();
        }
        else if (block.GetBType() == BlockType.Shrub)
        {
            return block.transform.GetChild(0).GetComponent<Shrub>().GetEnergyCost(characterManager.GetGatheringSkill());
        }
        else if (block.GetBType() == BlockType.Parts)
        {
            return Parts.GetEnergyCost(characterManager.GetTechnicalSkill());
        }
        else if (block.GetBType() == BlockType.Campfire)
        {
            return Campfire.GetEnergyCost(characterManager.GetCookingSkill());
        }
        else if (block.GetBType() == BlockType.Spaceship)
        {
            return Spaceship.GetEnergyCost(characterManager.GetTechnicalSkill());
        }
        else if (block.GetBType() == BlockType.Spider)
        {
            return Spider.GetEnergyCost(characterManager.GetGatheringSkill());
        }
        else return 0;
    }

    public void resetCosts()
    {
        characterEnergy.setTempEnergy(characterEnergy.GetEnergy());
    }
}
