using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class imageSelector : MonoBehaviour
{
    public void SelectImage(string imageName)
    {
        PlayerPrefs.SetString("SelectedImage", imageName);
        SceneManager.LoadScene("ARSceneFlora"); // Nama scene untuk AR
    }
}
