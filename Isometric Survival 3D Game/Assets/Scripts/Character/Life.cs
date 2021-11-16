using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Life : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image bar;

    float life, maxLife = 200;
    [SerializeField] float speed = 3f;
    float lerpSpeed;

    private void Start()
    {
        life = maxLife;
    }

    private void Update()
    {
        float percentage = life / maxLife * 100;
        text.text = percentage + "%";
        LifeBarFiller();
        lerpSpeed = speed * Time.deltaTime;
    }

    public float GetLife()
    {
        return life;
    }

    void LifeBarFiller()
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, (life / maxLife), lerpSpeed);
    }

    public void DecreaseLife(float points)
    {
        life -= points;
        if (life <= 0)
        {
            life = 0;
            Debug.Log("Game Over");
        }
    }

    public void IncreaseLife(float points)
    {
        life += points;
        if (life > maxLife) life = maxLife;
    }

}
