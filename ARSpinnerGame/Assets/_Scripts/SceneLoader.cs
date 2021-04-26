using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
    This singelton class can be loaded from anywhere
    https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/
*/
public class SceneLoader : Singleton<SceneLoader>
{
    //Scene being loaded
    private string sceneToBeLoaded;

    public void LoadScene(string _sceneName)
    {
        sceneToBeLoaded = _sceneName;


        StartCoroutine(InitializeSceneLoading());
    }



    IEnumerator InitializeSceneLoading()
    {

        //Load the loading scene
        yield return SceneManager.LoadSceneAsync("Scene_Loading");

        //Load the actual scene
        StartCoroutine(LoadActualScene());
      


    }

    IEnumerator LoadActualScene()
    {

        var asyncronousSceneLoading = SceneManager.LoadSceneAsync(sceneToBeLoaded);

        //this value stops the scene from displaying when it is still loading...
        asyncronousSceneLoading.allowSceneActivation = false;

        while (!asyncronousSceneLoading.isDone)
        {
            Debug.Log(asyncronousSceneLoading.progress);

            if (asyncronousSceneLoading.progress >= 0.9f )
            {
                // Show the scene 
                asyncronousSceneLoading.allowSceneActivation = true; 
            }


            yield return null;

        }


        

    }


}
