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
        equipment = FindObjectOfType<Equipment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eatFood()
    {
        life = characterManager.GetLife();
        if (equipment.GetFood() > 0)
        {
            equipment.RemoveFood(1);
            life.IncreaseLife(increasedLifeFood);
        }
    }

    public void eatCookedFood()
    {
        life = characterManager.GetLife();
        if (equipment.GetCookedFood() > 0)
        {
            equipment.RemoveCookedFood(1);
            life.IncreaseLife(increasedLifeCookedFood);
        }
    }

}
