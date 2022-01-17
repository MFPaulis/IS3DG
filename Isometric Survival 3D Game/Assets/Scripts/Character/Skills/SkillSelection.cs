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
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
