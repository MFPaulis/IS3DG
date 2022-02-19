using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CampEquipment : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI partsText;
    [SerializeField] TextMeshProUGUI foodText;
    [SerializeField] TextMeshProUGUI cookedFoodText;
    public static CampEquipment campEquipment;

    private void Awake()
    {
        campEquipment = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public TextMeshProUGUI GetWoodText()
    {
        return woodText;
    }

    public TextMeshProUGUI GetPartsText()
    {
        return partsText;
    }

    public TextMeshProUGUI GetFoodText()
    {
        return foodText;
    }

    public TextMeshProUGUI GetCookedFoodText()
    {
        return cookedFoodText;
    }
}
