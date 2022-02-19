using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EquipmentBackground : MonoBehaviour, IDropHandler
{
    CharacterManager characterManager;
    [SerializeField] int receiver;

    private void Start()
    {
        gameObject.SetActive(false);
        characterManager = FindObjectOfType<CharacterManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject ptrDrag = eventData.pointerDrag;
        if (ptrDrag != null)
        {
            Item item = ptrDrag.GetComponent<Item>();
            int owner = item.GetOwner();
            Equipment equipment;
            Equipment equipment2;
            if (owner == 0 || owner == 1)
            {
                equipment = characterManager.characters[owner].GetComponent<Equipment>();
            } else
            {
                equipment = Tent.activeTent.gameObject.GetComponent<Equipment>();
            }
            if (receiver == 0 || receiver == 1)
            {
                equipment2 = characterManager.characters[receiver].GetComponent<Equipment>();
            }
            else
            {
                equipment2 = Tent.activeTent.gameObject.GetComponent<Equipment>();
            }
            ItemType itemType = item.GetItemType();
            int maxItems = Math.Min(equipment.Get(itemType), equipment2.GetMax(itemType) - equipment2.Get(itemType));
            Transfer.Instance.Show(0, maxItems, owner, receiver, itemType);
            gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
