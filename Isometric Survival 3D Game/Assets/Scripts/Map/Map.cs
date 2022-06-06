using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Range(5, 100)]
    [SerializeField] int width;
    [Range(5, 100)]
    [SerializeField] int height;
    [SerializeField] Vector2Int startPos;
    [SerializeField] GameObject secretBlock;
    [SerializeField] GameObject blockTent;
    [SerializeField] GameObject blockFire;
    [SerializeField] GameObject [] blockPrefabs;
    [SerializeField] GameObject character1;
    [SerializeField] GameObject character2;
    [SerializeField] GameObject spaceship;
    [SerializeField] int[] probabilities;
    [SerializeField] int[] maxTimes;
    [SerializeField] GameObject[,] blocks;
    AnimalManager animalManager;
    int sumOfProbabilities;
    int[] times;
    List<GameObject> spaceshipBlocks = new List<GameObject>();
    Help help;

    // Start is called before the first frame update
    void Start()
    {
        help = FindObjectOfType<Help>();
        animalManager = FindObjectOfType<AnimalManager>();
        blocks = new GameObject[width, height];
        if (startPos.x > width - 5) startPos.x = width - 5;
        if (startPos.y > height - 5) startPos.y = height - 5;
        character1.transform.position = new Vector3(startPos.x + 1, 1, startPos.y + 1);
        character2.transform.position = new Vector3(startPos.x + 3, 1, startPos.y + 1);
        spaceship.transform.position = new Vector3(startPos.x + 1.97f, 1.57f, startPos.y + 2.95f);
        times = (int[]) maxTimes.Clone();
        foreach (int i in probabilities)
        {
            sumOfProbabilities += i;
        }
        GenerateArray();
        PlaceSpaceship();
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

    private void PlaceSpaceship()
    {
        for (int i = startPos.x + 1; i <= startPos.x + 4; i++)
        {
            for (int j = startPos.y + 3; j <= startPos.y + 4; j++ )
            {
                blocks[i, j].GetComponent<Block>().SetBType(BlockType.Spaceship);
                spaceshipBlocks.Add(blocks[i, j]);
            }
        }
    }

    public void Discover(int x, int z)
    {
        GameObject secretBlock = blocks[x, z];
        int chosenBlock = -1;
        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            if (times[i] == 0) chosenBlock = i;
            else times[i]--;
        }
        if (chosenBlock == -1)
        {
            chosenBlock = chooseRandomBlock();
        }
        times[chosenBlock] = maxTimes[chosenBlock];

        blocks[x, z] = Instantiate(blockPrefabs[chosenBlock], new Vector3(x, 0, z), Quaternion.identity);
        blocks[x, z].GetComponent<Node>().x = x;
        blocks[x, z].GetComponent<Node>().z = z;
        if(blocks[x,z].GetComponent<Block>().IsAnimal())
        {
            animalManager.AddAnimal(blocks[x,z].transform.GetChild(0).gameObject.GetComponent<Animal>());
        }
        secretBlock.GetComponent<SecretBlock>().CheckNeighbours();
        Destroy(secretBlock);
        switch (blocks[x,z].GetComponent<Block>().GetBType())
        {
            case BlockType.Woods:
                help.ShowHelp(1);
                break;
            case BlockType.Shrub:
                help.ShowHelp(2);
                break;
            case BlockType.Parts:
                help.ShowHelp(3);
                break;
            case BlockType.Spider:
                help.ShowHelp(4);
                break;
        }
    }

    private int chooseRandomBlock()
    {
        int rnd = Random.Range(0, sumOfProbabilities);
        int chosenBlock = -1;
        int sum = 0;
        do {
            chosenBlock += 1;
            sum += probabilities[chosenBlock];
        } while (sum < rnd);
        return chosenBlock;
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
        if(x < 0 || z < 0 || x >= width || z >= height)
        {
            return null;
        }
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

    public List<GameObject> GetSpaceshipBlocks()
    {
        return spaceshipBlocks;
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

        help.ShowHelp(5);
        help.ShowHelp(7);
    }
}
