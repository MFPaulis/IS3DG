using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    Map map;
    CharacterMovement characterMovement;
    int sightLevel;
    int charX, charZ;

    private void Start()
    {
        map = FindObjectOfType<Map>();
        characterMovement = FindObjectOfType<CharacterMovement>();
        Skillset skillset = FindObjectOfType<Skillset>();
        sightLevel = skillset.GetSightSkill();
    }

    public void LookAround()
    {
        charX = characterMovement.GetX();
        charZ = characterMovement.GetZ();
        if (sightLevel >= 1)
        {
            LookAtBlock(charX - 1, charZ);
            LookAtBlock(charX + 1, charZ);
            LookAtBlock(charX, charZ - 1);
            LookAtBlock(charX, charZ + 1);
        }
        if (sightLevel >= 2)
        {
            LookAtBlock(charX - 1, charZ - 1);
            LookAtBlock(charX + 1, charZ + 1);
            LookAtBlock(charX - 1, charZ + 1);
            LookAtBlock(charX + 1, charZ - 1);
        }
        if (sightLevel == 3)
        {
            LookAtBlock(charX - 2, charZ);
            LookAtBlock(charX + 2, charZ);
            LookAtBlock(charX, charZ - 2);
            LookAtBlock(charX, charZ + 2);
        }
    }

    void LookAtBlock(int x, int z)
    {
        if(map.IsPositionCorrect(x, z) )
        {
            GameObject block = map.GetBlock(x, z);
            if (block.GetComponent<SecretBlock>())
            {
                map.Discover(x, z);
            }
        }
    }
}
