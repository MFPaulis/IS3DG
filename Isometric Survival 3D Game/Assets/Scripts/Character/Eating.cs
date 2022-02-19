using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    CharacterManager characterManager;
    Equipment equipment;
    Life life;
    [SerializeField] float increasedLifeFood = 100;
    [SerializeField] float increasedLifeCookedFood = 200;

    // Start is called before the first frame update
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eatFood()
    {
        life = characterManager.GetLife();
        equipment = characterManager.GetEquipment();
        if (equipment.GetFood() > 0)
        {
            GetComponent<AudioSource>().Play();
            equipment.RemoveFood(1);
            life.IncreaseLife(increasedLifeFood);
        }
    }

    public void eatCookedFood()
    {
        life = characterManager.GetLife();
        equipment = characterManager.GetEquipment();
        if (equipment.GetCookedFood() > 0)
        {
            GetComponent<AudioSource>().Play();
            equipment.RemoveCookedFood(1);
            life.IncreaseLife(increasedLifeCookedFood);
        }
    }

}
