using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Energy : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image bar;

    float energy, maxEnergy = 200;
    [SerializeField] float speed = 3f;
    float lerpSpeed;

    private void Start()
    {
        energy = maxEnergy;

    }

    private void Update()
    {
        float percentage = energy / maxEnergy * 100;
        text.text = percentage + "%";
        EnergyBarFiller();
        lerpSpeed = speed * Time.deltaTime;
    }

    public float GetEnergy()
    {
        return energy;
    }

    void EnergyBarFiller()
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, (energy / maxEnergy), lerpSpeed);
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
