using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backNavigation : MonoBehaviour
{
    public void loadScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

}
