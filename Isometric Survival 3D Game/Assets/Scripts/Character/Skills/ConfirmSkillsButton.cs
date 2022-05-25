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
        StartCoroutine(LoadAsynchronously("SampleScene")); 
    }

   /* private void Update()
    {
        if (sceneLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
            SceneManager.UnloadSceneAsync("SkillSelection");
        }
    }*/

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);//, LoadSceneMode.Additive);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / .9f);
            //slider.value = progress;
            yield return null;
        }
        //sceneLoaded = true;
    }
}
