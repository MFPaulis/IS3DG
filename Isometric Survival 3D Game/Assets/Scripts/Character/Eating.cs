using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    Equipment equipment;
    Life life;
    [SerializeField] float increasedLife = 100;

    // Start is called before the first frame update
    void Start()
    {
        equipment = FindObjectOfType<Equipment>();
        life = FindObjectOfType<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eatFood()
    {
        if (equipment.GetFood() > 0)
        {
            equipment.RemoveFood(1);
            life.IncreaseLife(increasedLife);
        }
    }

    public void eatCookedFood()
    {
        if (equipment.GetCookedFood() > 0)
        {
            equipment.RemoveCookedFood(1);
            life.IncreaseLife(increasedLife);
        }
    }

}
