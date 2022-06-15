using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Animator animator;

    public Image ItemImage;

    public Sprite[] sprites;

    public void changeSprite(ItemType itemType)
    {
        ItemImage.sprite = sprites[((int)itemType)];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimFalse()
    {
        animator.SetBool("getNew", false);
    }

}
