using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform trueParent;
    private Transform dragParent;
    private RectTransform rectTransform;
    private CharacterManager characterManager;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    EquipmentBackground bg0, bg1, bg2;
    CharacterMovement charMov0, charMov1;
    Vector2 startPos;
    TimeManager timeManager;
    [SerializeField] int owner; //0,1,2 2-obóz
    [SerializeField] ItemType itemType;

    public Eating eating;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas_equipment").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        trueParent = transform.parent;
        dragParent = GameObject.Find("Dragged").transform; 
        startPos = transform.position;
        characterManager = FindObjectOfType<CharacterManager>();
        bg0 = GameObject.Find("Equipment_char1").transform.Find("Background").GetComponent<EquipmentBackground>();
        bg1 = GameObject.Find("Equipment_char2").transform.Find("Background").GetComponent<EquipmentBackground>();
        bg2 = CampEquipment.campEquipment.gameObject.transform.Find("Background").GetComponent<EquipmentBackground>();
        charMov0 = characterManager.characters[0].GetComponent<CharacterMovement>();
        charMov1 = characterManager.characters[1].GetComponent<CharacterMovement>();

        eating = GameObject.Find("GameManager").GetComponent<Eating>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(timeManager.IsDay())
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(timeManager.IsDay())
        {
            gameObject.transform.SetParent(dragParent, true);
            canvasGroup.blocksRaycasts = false;
            int char0x = charMov0.GetX();
            int char0z = charMov0.GetZ();
            int char1x = charMov1.GetX();
            int char1z = charMov1.GetZ();
            bool near02 = false;
            bool near12 = false;
            if (Tent.activeTent != null)
            {
                int tentx = (int)Math.Round(Tent.activeTent.gameObject.transform.position.x);
                int tentz = (int)Math.Round(Tent.activeTent.gameObject.transform.position.z);
                near02 = AreWeNear(char0x, char0z, tentx, tentz);
                near12 = AreWeNear(char1x, char1z, tentx, tentz);
            }
            bool near01 = AreWeNear(char0x, char0z, char1x, char1z);

            switch (owner)
            {
                case 0:
                    if (near01) bg1.Show();
                    if (near02) bg2.Show();
                    break;
                case 1:
                    if (near01) bg0.Show();
                    if (near12) bg2.Show();
                    break;
                case 2:
                    if (near02) bg0.Show();
                    if (near12) bg1.Show();
                    break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(trueParent, true);
        canvasGroup.blocksRaycasts = true;
        transform.position = startPos;
        CharacterImage charImage = characterManager.GetCharacterImage();
        switch(owner)
        {
            case 0:
                if (charImage.isMouseOver && owner == characterManager.GetCurrentCharacter())
                    eatItem();
                break;
            case 1:
                if (charImage.isMouseOver && owner == characterManager.GetCurrentCharacter())
                    eatItem();
                break;
            default:
                break;
        }

        bg0.Hide();
        bg1.Hide();
        bg2.Hide();
    }

    private void eatItem()
    {
        switch (itemType)
        {
            case ItemType.FOOD:
                eating.eatFood();
                break;
            case ItemType.COOKEDFOOD:
                eating.eatCookedFood();
                break;
            default:
                break;
        }
    }

    private bool AreWeNear(int x, int y, int x2, int y2)
    {
        if (x >= x2-1 && x <= x2+1 && y >= y2-1 && y <= y2+1)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public int GetOwner()
    {
        return owner;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
