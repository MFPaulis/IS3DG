using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Help : MonoBehaviour
{
    [SerializeField] List<GameObject> helpPictures;
    [SerializeField] GameObject button;
    Queue<int> helpPicturesQueue = new Queue<int>();
    List<bool> helpShown = new List<bool>();
    bool isPictureActive;
    int helpIndex;

    private void Start()
    {
        for (int i = 0; i < helpPictures.Count; i++)
            helpShown.Add(false);
        ShowHelp(0);
    }

    void Update()
    {
        if (helpPicturesQueue.Count != 0 && isPictureActive == false)
        {
            ShowHelpPicture(helpPicturesQueue.Dequeue());
        }
    }

    public void ShowHelp(int index)
    {
        helpPicturesQueue.Enqueue(index);
    }

    private void ShowHelpPicture(int index)
    {
        if (helpShown[index] == false)
        {
            isPictureActive = true;
            helpPictures[index].SetActive(true);
            button.SetActive(true);
            helpShown[index] = true;
            helpIndex = index;
        }
    }

    public void HideHelp()
    {
        helpPictures[helpIndex].SetActive(false);
        button.SetActive(false);
        isPictureActive=false;
    }
}
