using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSceneManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        Resources.UnloadUnusedAssets();
    }

    public void LoadSceneAsync(string sceneName, string activeSceneName)
    {
        var asyncOpp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneAsyncAdditive(string sceneName, string activeSceneName)
    {
        Resources.UnloadUnusedAssets();
        var asyncOpp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneAsyncAdditive(string sceneName, string activeSceneName, Action<AsyncOperation> callBack)
    {
        Resources.UnloadUnusedAssets();
        var asyncOpp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncOpp.completed += callBack;
    }

    public void UnloadSceneAsync(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void SetActiveScene(string activeSceneName)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeSceneName));
    }
}
