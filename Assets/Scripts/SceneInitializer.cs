using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    private AssetBundleDownloader _downloader;
    [SerializeField] private GroupSpawner _groupSpawner;
    [SerializeField] private BundleUrl[] _bundleUrls;
    [SerializeField] private List<AssetBundle> _bundles;

    private void Awake()
    {
        _bundles = new List<AssetBundle>();
        _downloader = FindObjectOfType<AssetBundleDownloader>();
        StartCoroutine(InitializeScene());
    }

    private IEnumerator InitializeScene()
    {
        //download bundles with bundle retrievers
        foreach (BundleUrl bundleUrl in _bundleUrls)
        {
            yield return _downloader.LoadBundleCoroutine(bundleUrl.Url, _bundles);
        }
        Debug.Log("Finished");
        //spawn objects AFTER the bundles are loaded
        //this should not pick up every other spawner
        foreach (AssetBundle bundle in _bundles)
        {
            _groupSpawner.SpawnAllChildren(bundle);
        }
    }

    private void OnDestroy()
    {
        _downloader.UnloadAndClearBundles(_bundles);
    }
}
