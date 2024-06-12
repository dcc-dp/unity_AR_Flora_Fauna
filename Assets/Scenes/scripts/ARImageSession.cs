using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARImagePopupController : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject popupPanel;
    public Text popupText; // Gunakan Text jika menggunakan UI Text
    // public TMPro.TextMeshProUGUI popupText; // Gunakan ini jika menggunakan TextMeshPro
    private string selectedImage;

    void Start()
    {
        popupPanel.SetActive(false);
        selectedImage = PlayerPrefs.GetString("SelectedImage", "");
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == selectedImage)
            {
                ShowPopup(trackedImage.transform.position, "Informasi untuk: " + selectedImage);
            }
        }
    }

    void ShowPopup(Vector3 position, string infoText)
    {
        popupText.text = infoText;
        popupPanel.transform.position = position;
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
