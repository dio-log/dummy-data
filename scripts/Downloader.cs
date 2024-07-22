

using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Downloader : MonoBehaviour
{

    private string path = "https://github.com/dio-log/dummy-data/raw/main/assetbundles/testbundle";
    private string bundleName = "test-bundle";
    private string savePath;

    void Awake()
    {
        savePath = Application.persistentDataPath + "/Bundles";
    }

    void Start()
    {
        StartCoroutine(GetBundle());
    }

    public IEnumerator GetBundle()
    {
         using (UnityWebRequest www = UnityWebRequest.Get(path))
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("다운로드 실패: " + www.error);
        }
        else
        {
            if(!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            // File.WriteAllBytes(savePath, www.downloadHandler.data);
            savePath += "/test-bundle";

            using(FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                byte[] assetBundleData = www.downloadHandler.data;
                fs.Write(assetBundleData, 0, assetBundleData.Length);
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(savePath);
            if (bundle != null)
            {
                string[] strings = bundle.GetAllAssetNames();
                foreach (var item in strings)
                {
                    Debug.Log("dd"+ item);
        
        
                }
                GameObject prefab = bundle.LoadAsset<GameObject>("tile");
                if(prefab != null)
                {
                    Object.Instantiate(prefab);
                    
                }
                else
                {
                    Debug.Log("에셋을 찾을 수 없습니다");
                }
                
                Debug.Log("AssetBundle 로드 성공");
                // 여기서 에셋 번들 사용
            }
            else
            {
                Debug.LogError("AssetBundle 로드 실패");
            }
        }
    }
    }

}