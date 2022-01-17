using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmSkillsButton : MonoBehaviour
{
    public void ConfirmSkills()
    {
        for (int i = 0; i < 2; i++)
        {
            if (SkillSelection.summary[i] > 5) return;
        }
        SceneManager.LoadScene("SampleScene");
    }
}
