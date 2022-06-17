using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmSkillsButton : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    
    //private bool sceneLoaded = false;

    public void ConfirmSkills()
    {
        for (int i = 0; i < 2; i++)
        {
            if (SkillSelection.summary[i] > 5) return;
        }
        transform.parent.gameObject.SetActive(false);
        //StartCoroutine(LoadAsynchronously("SampleScene")); 
    }

    /*
    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);//, LoadSceneMode.Additive);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            yield return null;
        }
    }*/
}
