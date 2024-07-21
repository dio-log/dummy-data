

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TreeEditor;
using Hierarchy;

public class JsonDownloader : MonoBehaviour
{
    public string jsonUrl = "https://raw.githubusercontent.com/dio-log/dummy-data/main/data/dummy.json";

    void Start()
    {
        StartCoroutine(DownloadJson());
    }

    IEnumerator DownloadJson()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(jsonUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || 
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonText = webRequest.downloadHandler.text;
                HierarchyNode root = JsonUtility.FromJson<HierarchyNode>(jsonText);
                Debug.Log("Downloaded JSON: " + jsonText);
                Debug.Log("Downloaded Node: " + root.name);

                
                // 여기에서 JSON 파싱 및 처리 로직을 추가할 수 있습니다.
            }
        }
    }
}