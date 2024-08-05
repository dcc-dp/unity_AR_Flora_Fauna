using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onOffMusic : MonoBehaviour
{

    private Button toggleButton;

    void Start()
    {
        toggleButton = GetComponent<Button>();
        toggleButton.onClick.AddListener(ToggleMusic);
    }

    void ToggleMusic()
    {
        setBackgroundMusic.Instance.ToggleMusic();
    }

}
