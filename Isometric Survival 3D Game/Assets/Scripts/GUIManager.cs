using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    public RectTransform character0;
    public RectTransform character1;

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
        character1.localScale = new Vector2(0.8f, 0.8f);
    }

    public void setActiveCharacter1()
    {
        character0.localScale = new Vector2(0.8f, 0.8f);
        character1.localScale = new Vector2(1, 1);
    }
}
