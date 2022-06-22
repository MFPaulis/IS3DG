using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private int currentCharacter;
    public GameObject[] characters;
    [SerializeField] GameObject[] cameras;
    [SerializeField] PathDrawing pathDrawing;
    [SerializeField] CharacterImage[] characterImages;
    TimeManager timeManager;
    public GUIManager GUIManager;
    Tutorial tutorial;

    GameObject circleChara1;
    GameObject circleChara2;

    private void Start()
    {
        tutorial = FindObjectOfType<Tutorial>();
        timeManager = FindObjectOfType<TimeManager>();
        cameras[0].SetActive(true);
        cameras[1].SetActive(false);

        circleChara1 = GameObject.Find("Cicle1");
        circleChara2 = GameObject.Find("Cicle2");
        circleChara2.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timeManager.IsDay())
        {
            pathDrawing.ErasePath();
            if(tutorial.GetStage() >= 6) changeCharacter();
        }
    }

    public void changeCharacter()
    {
        if (currentCharacter == 0)
        {
            GetEnergy().setTempEnergy(GetEnergy().GetEnergy());
            currentCharacter = 1;
            circleChara1.SetActive(false);
            circleChara2.SetActive(true);
            GUIManager.setActiveCharacter1();
        }
        else
        {
            GetEnergy().setTempEnergy(GetEnergy().GetEnergy());
            currentCharacter = 0;
            circleChara1.SetActive(true);
            circleChara2.SetActive(false);
            GUIManager.setActiveCharacter0();
        }

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

    public CharacterImage GetCharacterImage()
    {
        return characterImages[currentCharacter];
    }

    public int GetCurrentCharacter()
    {
        return currentCharacter;
    }

}
