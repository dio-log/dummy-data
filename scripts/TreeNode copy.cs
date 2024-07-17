using System.Collections;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;

public class ModelDownloader : MonoBehaviour
{
    private string cacheDirectory = Application.persistentDataPath + "/ModelCache/";

    void Start()
    {
        if (!Directory.Exists(cacheDirectory))
        {
            Directory.CreateDirectory(cacheDirectory);
        }

        StartCoroutine(DownloadAndExtractModel("modelId123"));
    }

    IEnumerator DownloadAndExtractModel(string modelId)
    {
        string url = "https://server.com/models/" + modelId + ".zip";
        string zipFilePath = cacheDirectory + modelId + ".zip";
        string extractedFolderPath = cacheDirectory + modelId;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllBytes(zipFilePath, www.downloadHandler.data);
            Debug.Log("Download complete: " + zipFilePath);

            ExtractZipFile(zipFilePath, extractedFolderPath);

            GameObject model = LoadModelFromExtractedFolder(extractedFolderPath);
            if (model != null)
            {
                Instantiate(model);
            }
        }
        else
        {
            Debug.LogError("Model download failed: " + www.error);
        }
    }

    // 압축 해제 메서드
    void ExtractZipFile(string zipFilePath, string destinationFolder)
    {
        if (Directory.Exists(destinationFolder))
        {
            Directory.Delete(destinationFolder, true);
        }
        ZipFile.ExtractToDirectory(zipFilePath, destinationFolder);
        Debug.Log("Extraction complete: " + destinationFolder);
    }

    GameObject LoadModelFromExtractedFolder(string folderPath)
    {
        // Implement your logic to load the model from the extracted files
        // For example, if you have a .obj file, you can use an asset loader
        string modelFilePath = Path.Combine(folderPath, "model.obj");
        if (File.Exists(modelFilePath))
        {
            // Load the model using your preferred method, e.g., OBJ loader
            GameObject model = OBJLoader.LoadOBJFile(modelFilePath);
            return model;
        }
        return null;
    }
}
