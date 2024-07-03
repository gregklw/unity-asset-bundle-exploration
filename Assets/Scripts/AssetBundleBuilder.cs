#if (UNITY_EDITOR) 
using System;
using UnityEngine;
using System.Linq;
using UnityEditor;
public static class AssetBundleBuilder
{
    private static string s_assetBundleDirectoryPath = "Assets/AssetBundles";
    private static string s_scriptableObjDirectoryPath = "Assets/ScriptableObjects";

    [MenuItem("Assets/Custom/Create Asset Bundle")]
    private static void BuildAllAssetBundles()
    {
        string assetBundleDirectoryPath = s_assetBundleDirectoryPath;
        Debug.Log(assetBundleDirectoryPath);

        try
        {
            string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
            int numberOfBundleNames = assetBundleNames.Length;
            AssetBundleBuild[] build = new AssetBundleBuild[numberOfBundleNames];
            for (int i = 0; i < numberOfBundleNames; i++)
            {
                string bundleName = assetBundleNames[i];
                build[i].assetBundleName = bundleName;
                string[] assetPathsFromBundle = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
                build[i].assetNames = assetPathsFromBundle;
                build[i].addressableNames = assetPathsFromBundle.Select(x => AssetDatabase.AssetPathToGUID(x)).ToArray();
            }

            foreach (var bundle in build)
            {
                for (int i = 0; i < bundle.addressableNames.Length; i++)
                {
                    Debug.Log($"Asset Name: {bundle.assetNames[i]} | GUID: {bundle.addressableNames[i]} | Variant name: {bundle.assetBundleVariant}");
                }
            }

            BuildAssetBundlesParameters parameters = new BuildAssetBundlesParameters()
            {
                outputPath = assetBundleDirectoryPath,
                options = BuildAssetBundleOptions.None,
                targetPlatform = EditorUserBuildSettings.activeBuildTarget,
                bundleDefinitions = build
            };
            BuildPipeline.BuildAssetBundles(parameters);

            //BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            foreach (string bundleName in assetBundleNames)
            {
                CreateUrlContainer(bundleName);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private static void CreateUrlContainer(string bundleName)
    {
        BundleUrl urlContainer = ScriptableObject.CreateInstance<BundleUrl>();
        string path = $"{s_scriptableObjDirectoryPath}/{bundleName}.asset";
        if (!System.IO.File.Exists(path))
        {
            AssetDatabase.CreateAsset(urlContainer, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = urlContainer;
        }
    }
}
#endif