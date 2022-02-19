using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Debug.Log("Drop!");
        GameObject ptrDrag = eventData.pointerDrag;
        if (ptrDrag != null)
        {
            Item item = ptrDrag.GetComponent<Item>();
            int owner = item.GetOwner();
            Equipment equipment;
            if (owner == 0 || owner == 1)
            {
                equipment = characterManager.characters[owner].GetComponent<Equipment>();
            } else
            {
                equipment = Tent.activeTent.gameObject.GetComponent<Equipment>();
            }
            ItemType itemType = item.GetItemType();
            int maxItems = equipment.GetWood();
            switch (itemType)
            {
                case ItemType.WOOD:
                    maxItems = equipment.GetWood();
                    break;
                case ItemType.PARTS:
                    maxItems = equipment.GetParts();
                    break;
                case ItemType.FOOD:
                    maxItems = equipment.GetFood();
                    break;
                case ItemType.COOKEDFOOD:
                    maxItems = equipment.GetCookedFood();
                    break;
            }
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
