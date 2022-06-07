using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public RectTransform character0;
    public RectTransform character1;

    public Image chara0Face;
    public Image chara0EnergyBar;
    public Image chara0LifeBar;
    public Image chara0What;


    public Image chara1Face;
    public Image chara1EnergyBar;
    public Image chara1LifeBar;
    public Image chara1What;

    // Start is called before the first frame update
    void Start()
    {
        setActiveCharacter0();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setActiveCharacter0()
    {
        character0.localScale = new Vector2(1, 1);
        character0.anchoredPosition = new Vector2(50, -61.25f);
        chara0Face.color = new Color32(255, 255, 255, 225);
        chara0EnergyBar.color = new Color32(58, 236, 25, 225);
        chara0LifeBar.color = new Color32(190, 28, 44, 225);

        character1.localScale = new Vector2(0.8f, 0.8f);
        character1.anchoredPosition = new Vector2(40.2f, -100);
        chara1Face.color = new Color32(255, 255, 255, 125);
        chara1EnergyBar.color = new Color32(83, 192, 83, 225);
        chara1LifeBar.color = new Color32(192, 78, 78, 125);


    }

    public void setActiveCharacter1()
    {
        character0.localScale = new Vector2(0.8f, 0.8f);
        character0.anchoredPosition = new Vector2(40.2f, -50);
        chara0Face.color = new Color32(255, 255, 255, 125);
        chara0EnergyBar.color = new Color32(83, 192, 83, 225);
        chara0LifeBar.color = new Color32(192, 78, 78, 125);

        character1.localScale = new Vector2(1, 1);
        character1.anchoredPosition = new Vector2(50, -100);
        chara1Face.color = new Color32(255, 255, 255, 225);
        chara1EnergyBar.color = new Color32(58, 236, 25, 225);
        chara1LifeBar.color = new Color32(190, 28, 44, 225);
    }
}
