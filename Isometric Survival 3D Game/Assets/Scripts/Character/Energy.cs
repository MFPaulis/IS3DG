using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Energy : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image bar;
    public Image barLess;

    float energy, tempEnergy, maxEnergy = 100;
    [SerializeField] float speed = 3f;
    float lerpSpeed;

    private void Start()
    {
        tempEnergy = maxEnergy;
        energy = maxEnergy;
    }

    private void Update()
    {
        float percentage = energy / maxEnergy * 100;
        text.text = percentage + "%";
        EnergyBarFiller();
        //EnergyLessBarFiller();
        lerpSpeed = speed * Time.deltaTime;
    }

    public float GetEnergy()
    {
        return energy;
    }

    void EnergyLessBarFiller()
    {
        barLess.fillAmount = Mathf.Lerp(barLess.fillAmount, (energy / maxEnergy), lerpSpeed);
    }

    void EnergyBarFiller()
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, (energy / maxEnergy), lerpSpeed);
    }

    public void setTempEnergy(float e)
    {
        tempEnergy = e;
    }

    public void DecreaseEnergy(float points)
    {
        energy -= points;
        if (energy < 0) energy = 0;
    }

    public void IncreaseEnergy(float points)
    {
        energy += points;
        if (energy > maxEnergy) energy = maxEnergy;
    }

}
