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
        // Memeriksa dan meminta izin penyimpanan saat aplikasi dimulai
        RequestStoragePermission();

        // Menambahkan listener untuk tombol download
        downloadButton.onClick.AddListener(OnDownloadButtonClicked);
    }

    void OnDownloadButtonClicked()
    {
        string sourcePath = GetStreamingAssetsPath(Path.Combine(folderName, pdfFileName));
        string destinationPath = GetRootStoragePath(pdfFileName);

        Debug.Log("Source path: " + sourcePath);
        Debug.Log("Destination path: " + destinationPath);

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
        // Menyimpan file ke folder Download
        string path = Path.Combine("/storage/emulated/0/Download", fileName);
        return path;
#else
        // Path alternatif untuk pengujian di Editor
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
                try
                {
                    File.WriteAllBytes(destinationPath, request.downloadHandler.data);
                    Debug.Log("File berhasil diunduh ke: " + destinationPath);
                    ShowToast("File berhasil diunduh! Lokasi: " + destinationPath);
                }
                catch (IOException ex)
                {
                    Debug.LogError("Gagal menulis file: " + ex.Message);
                    ShowToast("Gagal menulis file.");
                }
            }
            else
            {
                Debug.LogError("Gagal mengunduh file: " + request.error);
                ShowToast("Gagal mengunduh file.");
            }
        }
    }

    void RequestStoragePermission()
    {
#if UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageWrite))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
        }
#endif
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
