using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Transfer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Slider slider;
    CharacterManager characterManager;
    int sender;
    int receiver;
    ItemType itemType;
    Tutorial tutorial;

    public static Transfer Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        tutorial = FindObjectOfType<Tutorial>();
        gameObject.SetActive(false);
    }

    public void Show(int min, int max, int sender, int receiver, ItemType itemType)
    {
        slider.minValue = min;
        slider.maxValue = max;
        this.sender = sender;
        this.receiver = receiver;
        this.itemType = itemType;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateText()
    {
        text.text = slider.value.ToString();
    }

    public void SendItems()
    {
        int numberOfItems = (int)slider.value;
        Equipment senderEquipment;
        Equipment receiverEquipment;
        if (sender == 0 || sender == 1)
        {
            senderEquipment = characterManager.characters[sender].GetComponent<Equipment>();
        } else
        {
            senderEquipment = Tent.activeTent.GetComponent<Equipment>();
        }
        if (receiver == 0 || receiver == 1)
        {
            receiverEquipment = characterManager.characters[receiver].GetComponent<Equipment>();
        } else
        {
            receiverEquipment = Tent.activeTent.GetComponent<Equipment>();
        }

        switch (itemType)
        {
            case ItemType.WOOD:
                senderEquipment.RemoveWood(numberOfItems);
                receiverEquipment.AddWood(numberOfItems);
                if (receiverEquipment.GetWood() >= 5)
                {
                    tutorial.TutorialAction(9);
                }
                break;
            case ItemType.PARTS:
                senderEquipment.RemoveParts(numberOfItems);
                receiverEquipment.AddParts(numberOfItems);
                break;
            case ItemType.FOOD:
                tutorial.TutorialAction(7);
                senderEquipment.RemoveFood(numberOfItems);
                receiverEquipment.AddFood(numberOfItems);
                break;
            case ItemType.COOKEDFOOD:
                senderEquipment.RemoveCookedFood(numberOfItems);
                receiverEquipment.AddCookedFood(numberOfItems);
                break;
        }

        Hide();
    }

}
