using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ItemType {WOOD, PARTS, FOOD, COOKEDFOOD};

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

    public void setToCampEquipment()
    {
        CampEquipment campEquipment = CampEquipment.campEquipment;
        woodText = campEquipment.GetWoodText();
        partsText = campEquipment.GetPartsText();
        foodText = campEquipment.GetFoodText();
        cookedFoodText = campEquipment.GetCookedFoodText();
        woodText.text = wood.ToString();
        partsText.text = parts.ToString();
        foodText.text = food.ToString();
        cookedFoodText.text = cookedFood.ToString();
    }

    public void updateTexts()
    {
        woodText.text = wood.ToString();
        partsText.text = parts.ToString();
        foodText.text = food.ToString();
        cookedFoodText.text = cookedFood.ToString();
    }

    public int GetWood()
    {
        return wood;
    }

    public void AddWood(int howMany)
    {
        wood += howMany;
        woodText.text = wood.ToString();
    }

    public bool RemoveWood(int howMany)
    {
        if (wood - howMany < 0) return false;
        wood -= howMany;
        woodText.text = wood.ToString();
        return true;
    }

    public int GetParts()
    {
        return parts;
    }

    public void AddParts(int howMany)
    {
        parts += howMany;
        partsText.text = parts.ToString();
    }

    public bool RemoveParts(int howMany)
    {
        if (parts - howMany < 0) return false;
        parts -= howMany;
        partsText.text = parts.ToString();
        return true;
    }

    public int GetFood()
    {
        return food;
    }

    public void AddFood(int howMany)
    {
        food += howMany;
        foodText.text = food.ToString();
    }

    public bool RemoveFood(int howMany)
    {
        if (food - howMany < 0) return false;
        food -= howMany;
        foodText.text = food.ToString();
        return true;
    }

    public int GetCookedFood()
    {
        return cookedFood;
    }

    public void AddCookedFood(int howMany)
    {
        cookedFood += howMany;
        cookedFoodText.text = cookedFood.ToString();
    }

    public bool RemoveCookedFood(int howMany)
    {
        if (cookedFood - howMany < 0) return false;
        cookedFood -= howMany;
        cookedFoodText.text = cookedFood.ToString();
        return true;
    }
}
