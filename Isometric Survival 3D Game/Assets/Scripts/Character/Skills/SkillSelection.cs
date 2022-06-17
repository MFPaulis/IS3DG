using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelection : MonoBehaviour
{
    public static int[,] skills = new int[2, 4];
    public static int[] summary = new int[2];

    public static void sum(int character)
    {
        summary[character] = 0;
        for (int i = 0; i < 4; i++)
        {
            summary[character] += skills[character, i];
        }
    }

    public static void SetStartingSkills()
    {
        skills[0, 0] = 0;
        skills[0, 1] = 2;
        skills[0, 2] = 2;
        skills[0, 3] = 1;
        skills[1, 0] = 1;
        skills[1, 1] = 1;
        skills[1, 2] = 1;
        skills[1, 3] = 2;
    }

    public static void ResetSkills()
    {
        for (int i = 0; i < 2; i++)
            for(int j= 0; j < 4; j++)
                skills[i, j] = 0;
    }
}
