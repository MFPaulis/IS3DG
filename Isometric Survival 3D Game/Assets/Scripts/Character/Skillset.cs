using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillset : MonoBehaviour
{
    [Range(0,3)]
    [SerializeField] int sightSkill = 0;

    public int GetSightSkill()
    {
        return sightSkill;
    }
}
