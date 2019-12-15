using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static Scene LastSceneLoaded;
    public enum Scene {
        Main,
        MainMenu,
        LoadingScene,
        GameOver
    
    }
    private class LoadingMonoBehaviour : MonoBehaviour {}

    private static AsyncOperation loadingAsyncOperation;

    private static Action onLoaderCallback;
    public static void Load(Scene currentScene, Scene scene)
    {
        
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("LoadingGameObject");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));

        };
        LastSceneLoaded = currentScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }else
        {
            return 1f;
        }
    }
    public static void LoaderCallback()
    {
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
        
    }
}
