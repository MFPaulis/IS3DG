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
    Tutorial tutorial;

    string[] tutorialMap = new string[] {"             xxxxxxx",
                                        "       xxxxxxx.....x",
                                        "       x..S..x.....x",
                                        "       x*....x.....x",
                                        "       x.....x.....x",
                                        "       xxxxxxx.....x",
                                        "xxxxxxxxb.t.xxxxxxxx",
                                        "x *x.b.x.t..x       ",
                                        "x  x..bx...tx       ",
                                        "x  x.b.x.t..x       ",
                                        "xxxxxxxxt..*x       ",
                                        "       xxxxxx       "};

    // Start is called before the first frame update
    void Start()
    {
        tutorial = FindObjectOfType<Tutorial>();
        animalManager = FindObjectOfType<AnimalManager>();
        blocks = new GameObject[width, height];
        if (startPos.x > width - 5) startPos.x = width - 5;
        if (startPos.y > height - 5) startPos.y = height - 5;
        character1.transform.position = new Vector3(startPos.x - 13, 1, startPos.y - 3);
        character2.transform.position = new Vector3(startPos.x - 4, 1, startPos.y - 3);
        spaceship.transform.position = new Vector3(startPos.x + 1.97f, 1.57f, startPos.y + 2.95f);
        times = (int[]) maxTimes.Clone();
        foreach (int i in probabilities)
        {
            sumOfProbabilities += i;
        }
        GenerateArray();
        PrepareTutorial();
        //PlaceSpaceship();
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
        /*
        for (int i = startPos.x; i < startPos.x + 5; i++)
        {
            for (int j = startPos.y; j < startPos.y + 5; j++)
            {
                Clean(i, j);
            }
        }*/

        //Czyszczenie pocz¹tkowej lokacji
        for (int i = startPos.x - 13; i <= startPos.x - 12; i++)
        {
            for (int j = startPos.y - 4; j <= startPos.y - 3; j++)
            {
                Clean(i, j);
            }
        }
        Clean(startPos.x - 13, startPos.y - 2);
        //SetBlock(startPos.x - 12, startPos.y - 2, 2); //Ustawienie czêœci
    }

    public void SetBlock(int x, int z, int nr)
    {
        Destroy(blocks[x, z]);
        blocks[x, z] = Instantiate(blockPrefabs[nr], new Vector3(x, 0, z), Quaternion.identity);
        blocks[x, z].GetComponent<Node>().x = x;
        blocks[x, z].GetComponent<Node>().z = z;
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

    public void PrepareTutorial()
    {
        int k = 0;
        int l;
        for(int i = startPos.y + 5; i >= startPos.y - 6; i--)
        {
            l = 0;
            for (int j = startPos.x - 14; j <= startPos.x + 5; j++)
            {
                //Debug.Log("i " + i + " j " + j + " k " + k + " l " + l);
                switch (tutorialMap[k][l])
                {
                    case 'x':
                        blocks[j, i].GetComponent<SecretBlock>().SetBlocked(true);
                        break;
                    case '.':
                        blocks[j, i].GetComponent<SecretBlock>().SetType(BlockType.Empty);
                        break;
                    case '*':
                        blocks[j, i].GetComponent<SecretBlock>().SetType(BlockType.Parts);
                        break;
                    case 'b':
                        blocks[j, i].GetComponent<SecretBlock>().SetType(BlockType.Shrub);
                        break;
                    case 't':
                        blocks[j, i].GetComponent<SecretBlock>().SetType(BlockType.Woods);
                        break;
                    case 'S':
                        blocks[j, i].GetComponent<SecretBlock>().SetType(BlockType.Spider);
                        break;
                }
                l++;
            }
            k++;
        }
    }

    public void EndTutorial()
    {
        Debug.Log("END TUTORIAL");
        int k = 0;
        int l;
        for (int i = startPos.y + 5; i >= startPos.y - 6; i--)
        {
            l = 0;
            for (int j = startPos.x - 14; j <= startPos.x + 5; j++)
            {
                if (tutorialMap[k][l] == 'x' && blocks[j,i].GetComponent<SecretBlock>())
                {
                    blocks[j, i].GetComponent<SecretBlock>().SetBlocked(false);
                }
                l++;
            }
            k++;
        }
    }

    

    public void Discover(int x, int z)
    {
        GameObject secretBlock = blocks[x, z];
        if(!secretBlock.GetComponent<SecretBlock>().blocked)
        {
            secretBlock.GetComponent<SecretBlock>().ActionsOfPassages();
            int chosenBlock = -1;
            BlockType type = secretBlock.GetComponent<SecretBlock>().GetBlockType();
            if (type == BlockType.Secret)
            {
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
            }
            else
            {
                switch (type)
                {
                    case BlockType.Empty:
                        chosenBlock = 0;
                        break;
                    case BlockType.Woods:
                        chosenBlock = 1;
                        break;
                    case BlockType.Parts:
                        chosenBlock = 2;
                        break;
                    case BlockType.Shrub:
                        chosenBlock = 3;
                        break;
                    case BlockType.Spider:
                        chosenBlock = 4;
                        break;
                    default:
                        chosenBlock = 0;
                        break;
                }
            }
            blocks[x, z] = Instantiate(blockPrefabs[chosenBlock], new Vector3(x, 0, z), Quaternion.identity);
            blocks[x, z].GetComponent<Node>().x = x;
            blocks[x, z].GetComponent<Node>().z = z;
            if (blocks[x, z].GetComponent<Block>().IsAnimal())
            {
                animalManager.AddAnimal(blocks[x, z].transform.GetChild(0).gameObject.GetComponent<Animal>());
            }
            secretBlock.GetComponent<SecretBlock>().CheckNeighbours();
            Destroy(secretBlock);
        }
        
        /*switch (blocks[x,z].GetComponent<Block>().GetBType())
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
        }*/
    }

    public void ShowArea(int nr)
    {
        switch(nr)
        {
            case 0:
                for (int i = startPos.x - 10; i < startPos.x - 7; i++)
                    for (int j = startPos.y - 4; j < startPos.y - 1; j++)
                        Discover(i, j);
                break;
            case 1:
                Discover(startPos.x - 6, startPos.y - 3);
                Discover(startPos.x - 5, startPos.y - 3);
                Discover(startPos.x - 4, startPos.y - 3);
                break;
            case 2:
                for (int i = startPos.x - 5; i < startPos.x - 2; i++)
                    for (int j = startPos.y + 1; j < startPos.y + 4; j++)
                        Discover(i, j);
                break;
            case 3:
                for (int i = startPos.x; i < startPos.x + 5; i++)
                    for (int j = startPos.y; j < startPos.y + 5; j++)
                        Discover(i, j);
                PlaceSpaceship();
                break;
        }
    }

    public void UnlockPassage(int nr)
    {
        int x, z;
        switch(nr)
        {
            case 0:
                x = startPos.x - 11;
                z = startPos.y - 3;
                break;
            case 1:
                x = startPos.x - 7;
                z = startPos.y - 3;
                break;
            case 2:
                x = startPos.x - 4;
                z = startPos.y;
                break;
            default:
                x = startPos.x - 1;
                z = startPos.y + 2;
                break;
        }
        blocks[x, z].GetComponent<SecretBlock>().SetBlocked(false);
        blocks[x, z].GetComponent<SecretBlock>().SetType(BlockType.Empty);
        blocks[x, z].GetComponent<SecretBlock>().SetActivationArea(nr);
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

        tutorial.TutorialAction(10);

       // help.ShowHelp(5);
        // help.ShowHelp(7);
    }
}
