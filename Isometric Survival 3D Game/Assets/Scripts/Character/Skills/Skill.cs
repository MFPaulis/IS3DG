using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Skill : MonoBehaviour
{
    [SerializeField] int character;
    [SerializeField] int skillNumber;
    [SerializeField] TextMeshProUGUI info;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI summary;
    [SerializeField] string infoText;
    [SerializeField] string[] infoDescription;
    // Start is called before the first frame update
    void Start()
    {
        SkillSelection.skills[character, skillNumber] = 0;
    }

    public void UpdateSkill()
    {
        int value = (int) GetComponent<Slider>().value;
        info.text = infoText + value.ToString();
        description.text = infoDescription[value];
        SkillSelection.skills[character, skillNumber] = value;
        SkillSelection.sum(character);
        summary.text = SkillSelection.summary[character].ToString() + "/5";
        if (SkillSelection.summary[character] > 5)
        {
            summary.color = Color.red;
        }
        else
        {
            summary.color = Color.white;
        }
    }
}
