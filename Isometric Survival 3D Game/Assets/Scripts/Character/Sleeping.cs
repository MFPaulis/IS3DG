using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : MonoBehaviour
{
    Energy energy;
    Map map;
    CharacterMovement characterMovement;
    Life life;
    [SerializeField] float energyOutside = 100;
    [SerializeField] float energyInside = 200;
    [SerializeField] float decreasedLife = 40;
    // Start is called before the first frame update
    void Start()
    {
        energy = FindObjectOfType<Energy>();
        map = FindObjectOfType<Map>();
        characterMovement = FindObjectOfType<CharacterMovement>();
        life = FindObjectOfType<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep()
    {
        GameObject block = map.GetBlock(characterMovement.GetX(), characterMovement.GetZ());
        if(block.GetComponent<Tent>())
        {
            energy.IncreaseEnergy(energyInside);
        } else
        {
            energy.IncreaseEnergy(energyOutside);
        }
        life.DecreaseLife(decreasedLife);
        Shrub[] shrubs = FindObjectsOfType<Shrub>();
        foreach (Shrub shrub in shrubs)
        {
            shrub.CountDown();
        }
    }
}
