using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sleeping : MonoBehaviour
{
    CharacterManager characterManager;
    Energy energy;
    Map map;
    CharacterMovement characterMovement;
    Life life;
    [SerializeField] float energyOutside = 100;
    [SerializeField] float energyInside = 200;
    [SerializeField] float decreasedLife = 40;
    [SerializeField] TextMeshProUGUI daysText;
    int days = 0;
    Tutorial tutorial;

    // Start is called before the first frame update
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        map = FindObjectOfType<Map>();
        tutorial = FindObjectOfType<Tutorial>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Sleep()
    {
        tutorial.TutorialAction(2);
        days++;
        daysText.text = "day " + days;

        for (int i = 0; i < 2; i++)
        {
            characterMovement = characterManager.GetCharacterMovement();
            energy = characterManager.GetEnergy();
            life = characterManager.GetLife();
            GameObject block = map.GetBlock(characterMovement.GetX(), characterMovement.GetZ());
            if(block.GetComponent<Tent>())
            {
                energy.IncreaseEnergy(energyInside);
                tutorial.TutorialAction(12);
            } else
            {
                energy.IncreaseEnergy(energyOutside);
            }
            life.DecreaseLife(decreasedLife);
            characterManager.changeCharacter();
        }
        
        Shrub[] shrubs = FindObjectsOfType<Shrub>();
        foreach (Shrub shrub in shrubs)
        {
            shrub.CountDown();
        }
    }
}
