using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    bool isDay = true;
    [SerializeField] Image darkTexture;
    [SerializeField] Button foodButton;
    [SerializeField] Button cookedFoodButton;
    [SerializeField] Button finishButton;
    AnimalManager animalManager;

    private void Start()
    {
        animalManager = GetComponent<AnimalManager>();
    }

    public bool IsDay()
    {
        return isDay;
    }

    public void ChangeTime()
    {
        if (isDay)
        {
            isDay = false;
            foodButton.interactable = false;
            cookedFoodButton.interactable = false;
            finishButton.interactable = false;
            StartCoroutine(Fade(0.7f, 2.0f));
            StartCoroutine(Night());
        } else
        {
            isDay = true;
            foodButton.interactable = true;
            cookedFoodButton.interactable = true;
            finishButton.interactable = true;
            StartCoroutine(Fade(0.0f, 2.0f));
        }
    }

    IEnumerator Night()
    {
        animalManager.MoveAnimals();
        while(animalManager.IsMoving())
        {
            yield return new WaitForSeconds(1);
        }
        ChangeTime();
    }

    IEnumerator Fade(float aValue, float aTime)
    {
        if (!isDay)
        {
            darkTexture.gameObject.SetActive(true);
            //darkTexture.color = new Color(0, 0, 0, aValue);
        }
        float alpha = darkTexture.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            darkTexture.color = newColor;
            yield return null;
        }
        if (isDay)
        {
            darkTexture.gameObject.SetActive(false);
        }
    }
}
