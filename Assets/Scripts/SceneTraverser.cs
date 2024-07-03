using UnityEngine;

public class SceneTraverser : MonoBehaviour
{
    [SerializeField] private string[] _scenesToUnload;
    [SerializeField] private string[] _nextScenesToLoad;
    [SerializeField] private string _activeSceneName;
    [SerializeField] private bool _triggerOnStart;

    private BasicSceneManager _sceneManager;
    void Awake()
    {
        _sceneManager = FindObjectOfType<BasicSceneManager>();
    }
    private void Start()
    {
        if (_triggerOnStart) LoadNextScenes();
    }
    public void LoadNextScenes()
    {
        foreach (var scene in _scenesToUnload)
        {
            _sceneManager.UnloadSceneAsync(scene);
        }
        for (int i = 0; i < _nextScenesToLoad.Length; i++)
        {
            if (i == _nextScenesToLoad.Length)
            {
                System.Action<AsyncOperation> callBack = (value) => _sceneManager.SetActiveScene(_activeSceneName);
                _sceneManager.LoadSceneAsyncAdditive(_nextScenesToLoad[i], _activeSceneName, callBack);
            }
            else _sceneManager.LoadSceneAsyncAdditive(_nextScenesToLoad[i], _activeSceneName);
        }
    }
}
