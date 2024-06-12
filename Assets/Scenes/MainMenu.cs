using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Play;
    public GameObject Kuis;

    void Start()
    {
        Play.SetActive(true);
        Kuis.SetActive(false);
    }

    /*public void PlayClicked()
    {
        Application.LoadLevel("Menu");
    }
*/
    public void KuisClicked()
    {
        Play.SetActive(false);
        Kuis.SetActive(true);
    }



    /*public void MenuNav()
    {
        SceneManager.LoadScene("Menu");
    }

    public void KuisNav()
    {
        SceneManager.LoadScene("Kuis");
    }*/

}
