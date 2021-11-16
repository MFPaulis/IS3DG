using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Equipment : MonoBehaviour
{
    private int wood = 0;
    private int food = 0;
    private int cookedFood = 0;
    private int parts = 0;

    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI partsText;
    [SerializeField] TextMeshProUGUI foodText;
    [SerializeField] TextMeshProUGUI cookedFoodText;


    private void Update()
    {
        woodText.text = "wood: " + wood.ToString();
        partsText.text = "parts: " + parts.ToString();
        foodText.text = "food: " + food.ToString();
        cookedFoodText.text = "cooked food: " + cookedFood.ToString();
    }

    public int GetWood()
    {
        return wood;
    }

    public void AddWood(int howMany)
    {
        wood += howMany;
    }

    public bool RemoveWood(int howMany)
    {
        if (wood - howMany < 0) return false;
        wood -= howMany;
        return true;
    }

    public int GetParts()
    {
        return parts;
    }

    public void AddParts(int howMany)
    {
        parts += howMany;
    }

    public bool RemoveParts(int howMany)
    {
        if (parts - howMany < 0) return false;
        parts -= howMany;
        return true;
    }

    public int GetFood()
    {
        return food;
    }

    public void AddFood(int howMany)
    {
        food += howMany;
    }

    public bool RemoveFood(int howMany)
    {
        if (food - howMany < 0) return false;
        food -= howMany;
        return true;
    }

    public int GetCookedFood()
    {
        return cookedFood;
    }

    public void AddCookedFood(int howMany)
    {
        cookedFood += howMany;
    }

    public bool RemoveCookedFood(int howMany)
    {
        if (cookedFood - howMany < 0) return false;
        cookedFood -= howMany;
        return true;
    }
}
