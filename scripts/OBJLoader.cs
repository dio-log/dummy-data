using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Dummiesman; // ObjReader 라이브러리

public class ObjDownloader : MonoBehaviour
{
    public string objUrl = "http://example.com/model.obj";
    public string mtlUrl = "http://example.com/model.mtl"; // 재질 파일이 있는 경우

    IEnumerator Start()
    {
        // .obj 파일 다운로드
        using (UnityWebRequest www = UnityWebRequest.Get(objUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("OBJ 다운로드 실패: " + www.error);
            }
            else
            {
                string objData = www.downloadHandler.text;

                // .mtl 파일 다운로드 (있는 경우)
                string mtlData = null;
                if (!string.IsNullOrEmpty(mtlUrl))
                {
                    using (UnityWebRequest mtlWww = UnityWebRequest.Get(mtlUrl))
                    {
                        yield return mtlWww.SendWebRequest();

                        if (mtlWww.result == UnityWebRequest.Result.Success)
                        {
                            mtlData = mtlWww.downloadHandler.text;
                        }
                        else
                        {
                            Debug.LogWarning("MTL 다운로드 실패: " + mtlWww.error);
                        }
                    }
                }

                // OBJ 파일 파싱 및 게임 오브젝트 생성
                LoadAndRenderObj(objData, mtlData);
            }
        }
    }

    void LoadAndRenderObj(string objData, string mtlData)
    {
        // 문자열에서 OBJ 로드
        GameObject loadedObject = new ObjLoader().Load(objData, mtlData);

        // 생성된 오브젝트 위치 조정
        loadedObject.transform.position = Vector3.zero;

        // 필요한 경우 스케일 조정
        loadedObject.transform.localScale = Vector3.one;

        // 메시 렌더러 및 콜라이더 추가
        if (loadedObject.GetComponent<MeshRenderer>() == null)
        {
            loadedObject.AddComponent<MeshRenderer>();
        }
        if (loadedObject.GetComponent<MeshCollider>() == null)
        {
            loadedObject.AddComponent<MeshCollider>();
        }

        Debug.Log("OBJ 파일이 성공적으로 로드되고 렌더링되었습니다.");
    }
}