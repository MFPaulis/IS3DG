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

    [SerializeField] int maxWood = 8;
    [SerializeField] int maxFood = 8;
    [SerializeField] int maxCookedFood = 8;
    [SerializeField] int maxParts = 8;

    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI partsText;
    [SerializeField] TextMeshProUGUI foodText;
    [SerializeField] TextMeshProUGUI cookedFoodText;
    public GameObject popupParent;
    public Popup popupAdd;
    private Animator popupAnimator;


    private void Start()
    {
        updateTexts();
        popupAnimator = popupAdd.GetComponent<Animator>();
    }
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

    private void ActivateAnim()
    {
        popupParent.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        popupAnimator.SetBool("getNew", true);
    }
    public void updateTexts()
    {
        UpdateWood();
        UpdateParts();
        UpdateFood();
        UpdateCookedFood();
    }

    private void UpdateWood()
    {
        woodText.text = wood.ToString() + "/" + maxWood.ToString();
    }
    
    private void UpdateParts()
    {
        partsText.text = parts.ToString() + "/" + maxParts.ToString();
    }

    private void UpdateFood()
    {
        foodText.text = food.ToString() + "/" + maxFood.ToString();
    }

    private void UpdateCookedFood()
    {
        cookedFoodText.text = cookedFood.ToString() + "/" + maxCookedFood.ToString();
    }

    public int GetWood()
    {
        return wood;
    }

    public void AddWood(int howMany)
    {
        wood += howMany;
        popupAdd.changeSprite(ItemType.WOOD);
        ActivateAnim();
        if (wood > maxWood) wood = maxWood;
        UpdateWood();
    }

    public bool RemoveWood(int howMany)
    {
        if (wood - howMany < 0) return false;
        wood -= howMany;
        UpdateWood();
        return true;
    }

    public int GetParts()
    {
        return parts;
    }

    public void AddParts(int howMany)
    {
        parts += howMany;
        popupAdd.changeSprite(ItemType.PARTS);
        ActivateAnim();
        if (parts > maxParts) parts = maxParts;
        UpdateParts();
    }

    public bool RemoveParts(int howMany)
    {
        if (parts - howMany < 0) return false;
        parts -= howMany;
        UpdateParts();
        return true;
    }

    public int GetFood()
    {
        return food;
    }

    public void AddFood(int howMany)
    {
        food += howMany;
        popupAdd.changeSprite(ItemType.FOOD);
        ActivateAnim();
        if (food > maxFood) food = maxFood;
        UpdateFood();
    }

    public bool RemoveFood(int howMany)
    {
        if (food - howMany < 0) return false;
        food -= howMany;
        UpdateFood();
        return true;
    }

    public int GetCookedFood()
    {
        return cookedFood;
    }

    public void AddCookedFood(int howMany)
    {
        cookedFood += howMany;
        popupAdd.changeSprite(ItemType.COOKEDFOOD);
        ActivateAnim();
        if (cookedFood > maxCookedFood) cookedFood = maxCookedFood;
        UpdateCookedFood();
    }

    public bool RemoveCookedFood(int howMany)
    {
        if (cookedFood - howMany < 0) return false;
        cookedFood -= howMany;
        UpdateCookedFood();
        return true;
    }

    public int Get(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.WOOD:
                return wood;
            case ItemType.PARTS:
                return parts;
            case ItemType.FOOD:
                return food;
            case ItemType.COOKEDFOOD:
                return cookedFood;
        }
        return wood;
    }

    public int GetMax(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.WOOD:
                return maxWood;
            case ItemType.PARTS:
                return maxParts;
            case ItemType.FOOD:
                return maxFood;
            case ItemType.COOKEDFOOD:
                return maxCookedFood;
        }
        return maxWood;
    }


}
