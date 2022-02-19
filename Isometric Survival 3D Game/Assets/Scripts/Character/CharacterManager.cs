using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private int currentCharacter;
    public GameObject[] characters;
    [SerializeField] GameObject[] cameras;

    private void Start()
    {
        cameras[0].SetActive(true);
        cameras[1].SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeCharacter();
        }
    }

    public void changeCharacter()
    {
        if (currentCharacter == 0)
        {
            currentCharacter = 1;
        }
        else currentCharacter = 0;

        cameras[0].SetActive(currentCharacter == 0);
        cameras[1].SetActive(currentCharacter == 1);
    }

    public Energy GetEnergy()
    {
        return characters[currentCharacter].GetComponent<Energy>();
    }

    public Life GetLife()
    {
        return characters[currentCharacter].GetComponent<Life>();
    }

    public CharacterMovement GetCharacterMovement()
    {
        return characters[currentCharacter].GetComponent<CharacterMovement>();
    }

    public int GetSightSkill()
    {
        return SkillSelection.skills[currentCharacter, 0];
    }

    public int GetGatheringSkill()
    {
        return SkillSelection.skills[currentCharacter, 1];
    }

    public int GetTechnicalSkill()
    {
        return SkillSelection.skills[currentCharacter, 2];
    }

    public int GetCookingSkill()
    {
        return SkillSelection.skills[currentCharacter, 3];
    }

    public GameObject GetCamera()
    {
        return cameras[currentCharacter];
    }

    public Equipment GetEquipment()
    {
        return characters[currentCharacter].GetComponent<Equipment>();
    }

}
