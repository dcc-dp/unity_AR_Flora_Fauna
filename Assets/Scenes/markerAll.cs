using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class markerAll : MonoBehaviour
{
    public Button downloadButton;
    private string pdfFileName = "newMarker.pdf"; // Nama file PDF Anda
    private string folderName = "marker"; // Nama subfolder di StreamingAssets

    void Start()
    {
        downloadButton.onClick.AddListener(OnDownloadButtonClicked);
    }

    void OnDownloadButtonClicked()
    {
        string sourcePath = GetStreamingAssetsPath(Path.Combine(folderName, pdfFileName));
        string destinationPath = GetRootStoragePath(pdfFileName);

        Debug.Log("File akan diunduh dari: " + sourcePath);
        Debug.Log("File akan diunduh ke: " + destinationPath);
        StartCoroutine(DownloadFile(sourcePath, destinationPath));
    }

    string GetStreamingAssetsPath(string relativePath)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Path.Combine(Application.streamingAssetsPath, relativePath);
#else
        return "file://" + Path.Combine(Application.streamingAssetsPath, relativePath);
#endif
    }

    string GetRootStoragePath(string fileName)
    {
#if UNITY_ANDROID
        return Path.Combine("/storage/emulated/0/", fileName); // Path umum untuk penyimpanan internal Android
#else
        return Path.Combine(Application.persistentDataPath, fileName);
#endif
    }

    System.Collections.IEnumerator DownloadFile(string sourcePath, string destinationPath)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(sourcePath))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                File.WriteAllBytes(destinationPath, request.downloadHandler.data);
                Debug.Log("File berhasil diunduh ke: " + destinationPath);
                ShowToast("File berhasil diunduh! Lokasi: " + destinationPath);
            }
            else
            {
                Debug.LogError("Gagal mengunduh file: " + request.error);
                ShowToast("Gagal mengunduh file.");
            }
        }
    }

    void ShowToast(string message)
    {
#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
#endif
    }
}
