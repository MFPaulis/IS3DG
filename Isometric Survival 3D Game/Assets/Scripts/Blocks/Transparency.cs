using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Transparency : MonoBehaviour
{
    public GameObject popup;

    private void Start()
    {
        popup.SetActive(false);
    }


    private void OnMouseOver()
    {
        Material[] materials = GetComponent<Renderer>().materials;
        for(int i = 0; i < materials.Length; i++)
        {
            float colorR = materials[i].GetColor("_Color").r;
            float colorG = materials[i].GetColor("_Color").g;
            float colorB = materials[i].GetColor("_Color").b;
            materials[i].color = new Color(colorR, colorG, colorB, 0.5f);
            materials[i].SetFloat("_Opacity", 0.5f);
            popup.SetActive(true);
        }
    }

    
    private void OnMouseExit()
    {
        Material[] materials = GetComponent<Renderer>().materials;
        for (int i = 0; i < materials.Length; i++)
        {
            float colorR = materials[i].GetColor("_Color").r;
            float colorG = materials[i].GetColor("_Color").g;
            float colorB = materials[i].GetColor("_Color").b;
            materials[i].color = new Color(colorR, colorG, colorB, 1.0f);
            materials[i].SetFloat("_Opacity", 1.0f);
            popup.SetActive(false);
        }
    }
}
