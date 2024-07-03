using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleDownloader : MonoBehaviour
{
    //public List<AssetBundle> bundles = new List<AssetBundle>();

    public IEnumerator LoadBundleCoroutine(string bundleUrl, List<AssetBundle> bundles)
    {
        //GameObject go = null;
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning($"Error on the get request at: {bundleUrl} {www.error}");
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                bundles.Add(bundle);
                Debug.Log($"Loading {bundle}");
                //go = bundle.LoadAsset(guid) as GameObject;
                //bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
        //if (go != null) break;

        //if (go == null)
        //{
        //    Debug.LogWarning("GameObject is null.");
        //}
        //else callback?.Invoke(go);
        //PrintAssetValues();
    }
    //public GameObject InstantiateLoadedGameObject(string guid)
    //{
    //    PrintAssetValues();
    //    if (go == null)
    //    {
    //        Debug.LogWarning("GameObject is null.");
    //        return null;
    //    }
    //    return Instantiate(go, Vector3.zero, Quaternion.identity);
    //}

    //public void PrintAssetValues()
    //{
    //    foreach (var pair in _assetDictionary) {
    //        Debug.Log($"Key: {pair.Key} | Value: {pair.Value}");
    //    }
    //}

    public GameObject LoadGameObject(string guid, List<AssetBundle> bundles)
    {
        GameObject go = null;
        foreach (AssetBundle bundle in bundles)
        {
            go = LoadGameObject(guid, bundle);
            if (go != null) break;
        }
        return go;
    }

    public GameObject LoadGameObject(string guid, AssetBundle bundle)
    {
        GameObject go = bundle.LoadAsset(guid) as GameObject;
        return go;
    }

    //public void LoadBundle()
    //{
    //    foreach (var bundle in bundles)
    //    {
    //        bundle.LoadAllAssetsAsync();
    //    }
    //}

    public void UnloadAndClearBundle(AssetBundle bundle)
    {
        bundle.UnloadAsync(true);
    }

    public void UnloadAndClearBundles(List<AssetBundle> bundles)
    {
        foreach (var bundle in bundles)
        {
            UnloadAndClearBundle(bundle);
        }
        bundles.Clear();
    }
}
