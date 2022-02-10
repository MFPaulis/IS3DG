using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {Empty, Woods, Shrub, Parts, Tent, Campfire, Spaceship};

public class Block : MonoBehaviour
{
    [SerializeField] BlockType bType;

    public BlockType GetBType()
    {
        return bType;
    }

    public void BlockClicked()
    {
        if (bType == BlockType.Empty)
        {
            GetComponent<EmptyPlace>().Clicked();
        } else if (bType == BlockType.Woods)
        {
            transform.GetChild(0).GetComponent<Woods>().Clicked();
        } else if (bType == BlockType.Shrub)
        {
            transform.GetChild(0).GetComponent<Shrub>().Clicked();
        } else if (bType == BlockType.Parts)
        {
            transform.GetChild(0).GetComponent<Parts>().Clicked();
        } else if (bType == BlockType.Tent)
        {
            GetComponent<Tent>().Clicked();
        }
        else if (bType == BlockType.Campfire)
        {
            GetComponent<Campfire>().Clicked();
        } else if (bType == BlockType.Spaceship)
        {
            int x = GetComponent<Node>().x;
            int z = GetComponent<Node>().z;
            FindObjectOfType<Spaceship>().Clicked(x, z);
        }
    }

    public void SetBType(BlockType type)
    {
        bType = type;
    }


}
