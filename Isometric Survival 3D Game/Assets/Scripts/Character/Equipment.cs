using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Equipment : MonoBehaviour
{
    private int wood = 0;
    /*private int food = 0;
    private int cookedFood = 0;*/
    private int parts = 0;

    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI partsText;

    private void Update()
    {
        woodText.text = "wood: " + wood.ToString();
        partsText.text = "parts: " + parts.ToString();

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


}
