using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Range(5, 20)]
    [SerializeField] int width;
    [Range(5, 20)]
    [SerializeField] int height;
    [SerializeField] Vector2Int startPos;
    [SerializeField] GameObject secretBlock;
    [SerializeField] GameObject blockTent;
    [SerializeField] GameObject blockFire;
    [SerializeField] GameObject [] blockPrefabs;
    [SerializeField] GameObject[,] blocks;

    // Start is called before the first frame update
    void Start()
    {
        blocks = new GameObject[width, height];
        if (startPos.x > width - 5) startPos.x = width - 5;
        if (startPos.y > height - 5) startPos.y = height - 5;
        GenerateArray();
        
    }


    private void GenerateArray()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                blocks[i, j] = Instantiate(secretBlock, new Vector3(i, 0, j), Quaternion.identity);
                blocks[i, j].GetComponent<Node>().x = i;
                blocks[i, j].GetComponent<Node>().z = j;
            }
        }
        for (int i = startPos.x; i < startPos.x + 5; i++)
        {
            for (int j = startPos.y; j < startPos.y + 5; j++)
            {
                Clean(i, j);
            }
        }
    }

    public void Discover(int x, int z)
    {
        GameObject secretBlock = blocks[x, z];
        int rnd = Random.Range(0, blockPrefabs.Length);
        blocks[x, z] = Instantiate(blockPrefabs[rnd], new Vector3(x, 0, z), Quaternion.identity);
        blocks[x, z].GetComponent<Node>().x = x;
        blocks[x, z].GetComponent<Node>().z = z;
        secretBlock.GetComponent<SecretBlock>().CheckNeighbours();
        Destroy(secretBlock);

    }

    public bool IsPositionCorrect(int x, int z)
    {
        if (x < 0 || z < 0 || x >= width || z >= height) return false;
        else return true;
    }

    public Node GetNode(int x, int z)
    {
        return blocks[x, z].GetComponent<Node>();
    }

    public GameObject GetBlock(int x, int z)
    {
        return blocks[x, z];
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public void Clean(int x, int z)
    {
        Destroy(blocks[x, z]);
        blocks[x, z] = Instantiate(blockPrefabs[0], new Vector3(x, 0, z), Quaternion.identity);
        blocks[x, z].GetComponent<Node>().x = x;
        blocks[x, z].GetComponent<Node>().z = z;
    }

    public void SetCamp(int x, int z, int xShift, int zShift)
    {
        Destroy(blocks[x, z]);
        blocks[x, z] = Instantiate(blockTent, new Vector3(x, 0, z), Quaternion.identity);
        blocks[x, z].GetComponent<Node>().x = x;
        blocks[x, z].GetComponent<Node>().z = z;

        x = x + xShift;
        z = z + zShift;

        Destroy(blocks[x, z]);
        blocks[x, z] = Instantiate(blockFire, new Vector3(x, 0, z), Quaternion.identity);
        blocks[x, z].GetComponent<Node>().x = x;
        blocks[x, z].GetComponent<Node>().z = z;
        blocks[x, z].GetComponent<Node>().walkable = false;
    }
}
