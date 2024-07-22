using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundle
{
    [MenuItem("Assets/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        string dir = "Assets/AssetBundles";
        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }

    [MenuItem("Assets/Get AssetBundle names")]
    public static void GetNames()
    {
        string[] names = AssetDatabase.GetAllAssetBundleNames();
        foreach (string name in names)
        {
            Debug.Log("AssetBundle : "+name);
        }

    }
}
