using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    private List <Animal> animals = new List <Animal> ();

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
    }

    public void RemoveAnimal(Animal animal)
    {
        animals.Remove(animal);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveAnimals()
    {
        foreach (Animal animal in animals)
        {
            animal.Move();
        }
    }

    public bool IsMoving()
    {
        bool isMoving = false;
        foreach (Animal animal in animals)
        {
            if (animal.gameObject.GetComponent<AnimalMovement>().IsMoving())
            {
                isMoving = true;
                break;
            }
        }
        return isMoving;
    }
}

